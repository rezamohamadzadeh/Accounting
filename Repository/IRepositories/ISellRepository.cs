using DAL.Models;
using Repository.InterFace;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface ISellRepository : IGenericRepository<Tb_Sell>
    {
        List<Tb_Sell> Filter(string draw, string length, string sortColumn, string sortColumnDirection, string searchValue, int pageSize, int skip, ref int recordsTotal, string userEmail);
        IEnumerable<Tb_Sell> GetSellOnDashboard(int count, string UserId, ref string affiliateCode, int filterValue = 0);
        List<string> AffiliateCustomers(string draw, string length, string sortColumn, string sortColumnDirection, string searchValue, int pageSize, int skip, ref int recordsTotal, string userEmail);
    }
}
