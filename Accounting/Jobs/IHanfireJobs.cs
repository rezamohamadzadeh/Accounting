
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace Accounting.Jobs
{
    public interface IHanfireJobs
    {
        Task AccountingAffiliateAmountSell();
        void BackupFromDataBase(DatabaseName databaseName);
        void DeleteLocalFolderContentByDateRange(DateTime startDate, DateTime endDate, string folderName);
        void DeleteRemoteFolderContentByDateRange(DateTime startDate, DateTime endDate, string remoteAddress, string userName, string Password);
    }

}
