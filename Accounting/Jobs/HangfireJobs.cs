﻿using Accounting.Models;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository.InterFace;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Accounting.Jobs
{
    /// <summary>
    /// Implement Jobs in hangfire
    /// </summary>
    public class HanfireJobs : IHanfireJobs
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _client;
        public HanfireJobs(IUnitOfWork uow, IEmailSender emailSender, UserManager<ApplicationUser> userManager,IConfiguration config,IHttpClientFactory client)
        {
            _uow = uow;
            _emailSender = emailSender;
            _userManager = userManager;
            _config = config;
            _client = client;
        }

        /// <summary>
        /// Calc AffiliateAmountSell
        /// </summary>
        /// <returns></returns>
        public async Task AccountingAffiliateAmountSell()
        {
            var affiliates = _uow.AffiliateRepo.Get();

            foreach (var item in affiliates)
            {
                var affiliatecomision = _uow.AffiliateRepo.Get(d => d.Email == item.Email).FirstOrDefault();
                var Comision = affiliatecomision == null ? 0 : affiliatecomision.comision;
                var affiliateSell = _uow.SellRepo.Get(d => d.AffiliateCode == item.Code && d.PayStatus != PayStatus.Registered && d.CreateAt > DateTime.Now.AddDays(-1)).Select(d => new Tb_Sell { Price = d.Price }).ToList();
                var sumSell = affiliateSell == null ? 0 : affiliateSell.Sum(d => d.Price);

                var CommisionAmount = (sumSell * Comision) / 100;
                await _emailSender.SendEmailAsync(item.Email.Trim(), "Your Today Ammount Comission", "Your Today Ammount Comission Is : " + CommisionAmount.ToString());
            }
        }

        /// <summary>
        /// Delete local file on server
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="folderName"></param>
        public void DeleteLocalFolderContentByDateRange(DateTime startDate, DateTime endDate, string folderName)
        {
            string[] files = Directory.GetFiles("wwwroot/" + folderName);
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.CreationTime.Date >= startDate.Date && fi.CreationTime.Date <= endDate.Date)
                    fi.Delete();
            }
        }

        /// <summary>
        /// Delete remote files on ftp server 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="remoteAddress"></param>
        public void DeleteRemoteFolderContentByDateRange(DateTime startDate,
            DateTime endDate, string remoteAddress, string userName, string Password)
        {
            remoteAddress += "/";
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteAddress);
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            NetworkCredential credentials = new NetworkCredential(userName, Password);
            FtpWebRequest listRequest = (FtpWebRequest)WebRequest.Create(remoteAddress);
            listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            listRequest.Credentials = credentials;

            List<string> lines = new List<string>();

            using (FtpWebResponse listResponse = (FtpWebResponse)listRequest.GetResponse())
            using (Stream listStream = listResponse.GetResponseStream())
            using (StreamReader listReader = new StreamReader(listStream))
            {
                while (!listReader.EndOfStream)
                {
                    lines.Add(listReader.ReadLine());
                }
            }

            foreach (string line in lines)
            {
                string[] tokens =
                    line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[3];
                string date = tokens[0];

                string fileUrl = remoteAddress + name;

                DateTime FileDate = DateTime.Parse(date);
                if (FileDate >= startDate && FileDate <= endDate)
                {
                    FtpWebRequest deleteRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                    deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                    deleteRequest.Credentials = credentials;

                    deleteRequest.GetResponse();
                }
            }

        }

        /// <summary>
        /// Backup From Db
        /// </summary>
        public void BackupFromDataBase(DatabaseName databaseName)
        {
            _uow.BackUpFromDb(databaseName);
        }

        /// <summary>
        /// Send Affiliate sell report to admins
        /// </summary>
        /// <param name="wwwroot">for pdf generator setting for example: header image and fonts</param>
        /// <returns></returns>
        public async Task SendAffiliatesSellReportToAdmin()
        {
            List<AffiliateReportDto> list = new List<AffiliateReportDto>();
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var affiliateReportFile = await GetAffiliateReportApi("Weekly", "Admin");
            foreach (var admin in adminUsers)
            {
                var pdfFile = affiliateReportFile;
                await _emailSender.SendEmailWithMemoryStreamFileAsync(admin.Email, "AffiliateReport", "This email is affiliates sell report", Guid.NewGuid().ToString() + ".pdf", "Application/Pdf", pdfFile);
            }

        }
        private async Task<byte[]> GetAffiliateReportApi(string filterDate, string userType, string email = null)
        {
            var url = _config["BaseApiUrl"];
            url += string.Format("/api/Affiliate/GetAffiliateUsers?filterDate={0}&userType={1}&email={2}", filterDate, userType, email);
            var api = _client.CreateClient();
            try
            {
                HttpResponseMessage messages = await api.GetAsync(url);

                if (messages.IsSuccessStatusCode)
                {
                    var contentResult = await messages.Content.ReadAsAsync<JsonResultContent<byte[]>>();
                    return contentResult.Data;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// ChangeUSDToNewCurrency - With this job you can change product prices by usd currency to other currency prices
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>

        public async Task ChangeUSDToNewCurrencyJob(string currency,double amount)
        {
            var url = _config["BaseApiUrl"];
            url += string.Format("/api/ConvertCurrency?currency={0}&amount={1}",currency,amount);

            var api = _client.CreateClient();
            try
            {
                HttpResponseMessage messages = await api.GetAsync(url);

                if (messages.IsSuccessStatusCode)
                {
                    var contentResult = await messages.Content.ReadAsAsync<CurrencyDto>();
                    var currencyResult = contentResult.result;
                    var products = _uow.ProductDetailRepo.Get();

                    // Change New price by new currency

                    foreach (var item in products)
                    {
                        var newPrice = (item.Price * currencyResult) / amount;
                        item.Price = newPrice;
                        _uow.ProductDetailRepo.Update(item);
                        await _uow.SaveAsync();
                    }
                }
                
            }
            catch (Exception ex)
            {
                
            }

        }

    }
}
