
using System;
using System.Threading.Tasks;

namespace Accounting.Jobs
{
    public interface IHanfireJobs
    {
        Task AccountingAffiliateAmountSell();
        void DeleteFolderContentByDateRange(DateTime startDate, DateTime endDate, string folderName);
    }

}
