using DAL.Models;
using Repository.InterFace;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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


        public HanfireJobs(IUnitOfWork uow, IEmailSender emailSender)
        {
            _uow = uow;
            _emailSender = emailSender;
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
            DateTime endDate, string remoteAddress,string userName,string Password)
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

                //if (permissions[0] == 'd')
                //{
                //    DeleteFtpDirectory(fileUrl + "/", credentials);
                //}
                //else
                //{

                //    FtpWebRequest deleteRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                //    deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                //    deleteRequest.Credentials = credentials;

                //    deleteRequest.GetResponse();
                //}
                DateTime FileDate = DateTime.Parse(date);
                if (FileDate >= startDate && FileDate <= endDate)
                {
                    FtpWebRequest deleteRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                    deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                    deleteRequest.Credentials = credentials;

                    deleteRequest.GetResponse();
                }
            }

            //FtpWebRequest removeRequest = (FtpWebRequest)WebRequest.Create(url);
            //removeRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
            //removeRequest.Credentials = credentials;

            //removeRequest.GetResponse();
        }

        /// <summary>
        /// Backup From Db
        /// </summary>
        public void BackupFromDataBase(DatabaseName databaseName)
        {
            _uow.BackUpFromDb(databaseName);
        }
    }
}
