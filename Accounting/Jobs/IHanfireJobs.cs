
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace Accounting.Jobs
{
    public interface IHanfireJobs
    {
        Task AccountingAffiliateAmountSell();
        Task SendAffiliatesSellReportToAdmin();
        void BackupFromDataBase(DatabaseName databaseName);
        Task ChangeUSDToNewCurrencyJob(string currency, double amount);
        void DeleteLocalFolderContentByDateRange(DateTime startDate, DateTime endDate, string folderName);
        void DeleteRemoteFolderContentByDateRange(DateTime startDate, DateTime endDate, string remoteAddress, string userName, string Password);
    }

}
