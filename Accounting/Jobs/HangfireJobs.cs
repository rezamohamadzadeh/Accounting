using DAL.Models;
using Repository.InterFace;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public void DeleteFolderContentByDateRange(DateTime startDate, DateTime endDate, string folderName)
        {
            string[] files = Directory.GetFiles("wwwroot/" + folderName);
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.CreationTime > startDate && fi.CreationTime < endDate)
                    fi.Delete();
            }
        }
    }
}
