using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SellRepository : GenericRepositori<Tb_Sell>, ISellRepository
    {
        public SellRepository(ApplicationDbContext Db) : base(Db)
        { }


        /// <summary>
        /// get affiliate sells in listview datatable
        /// </summary>
        /// <param name="draw"></param>
        /// <param name="length"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortColumnDirection"></param>
        /// <param name="searchValue"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="recordsTotal"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public List<Tb_Sell> Filter(string draw,
           string length,
           string sortColumn,
           string sortColumnDirection,
           string searchValue,
           int pageSize,
           int skip,
           ref int recordsTotal,
           string userEmail)
        {
            IQueryable<Tb_Sell> query = _dbset;
            
            var affiliatCode = _context.Tb_Affiliates.FirstOrDefault(d => d.Email == userEmail);

            if (affiliatCode == null)
                return null;

            query = query.Where(d => d.AffiliateCode == affiliatCode.Code);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                if (sortColumnDirection == "asc")
                {
                    switch (sortColumn)
                    {
                        case "Email":
                            query = query.OrderBy(d => d.Email);
                            break;
                        case "FullName":
                            query = query.OrderBy(d => d.FullName);
                            break;
                        case "ProductName":
                            query = query.OrderBy(d => d.ProductName);
                            break;
                        case "Price":
                            query = query.OrderBy(d => d.Price);
                            break;
                        case "AffiliateCode":
                            query = query.OrderBy(d => d.AffiliateCode);
                            break;
                        case "PayStatus":
                            query = query.OrderBy(d => d.PayStatus);
                            break;
                        case "DiliveryStatus":
                            query = query.OrderBy(d => d.DiliveryStatus);
                            break;
                        case "CreateAt":
                            query = query.OrderBy(d => d.CreateAt);
                            break;

                        default:
                            break;
                    }
                }
                if (sortColumnDirection == "desc")
                {
                    switch (sortColumn)
                    {
                        case "Email":
                            query = query.OrderByDescending(d => d.Email);
                            break;
                        case "FullName":
                            query = query.OrderByDescending(d => d.FullName);
                            break;
                        case "ProductName":
                            query = query.OrderByDescending(d => d.ProductName);
                            break;
                        case "Price":
                            query = query.OrderByDescending(d => d.Price);
                            break;
                        case "AffiliateCode":
                            query = query.OrderByDescending(d => d.AffiliateCode);
                            break;
                        case "PayStatus":
                            query = query.OrderByDescending(d => d.PayStatus);
                            break;
                        case "DiliveryStatus":
                            query = query.OrderByDescending(d => d.DiliveryStatus);
                            break;
                        case "CreateAt":
                            query = query.OrderByDescending(d => d.CreateAt);
                            break;
                        default:
                            break;
                    }
                }
            }


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(m => m.AffiliateCode == searchValue);
            }

            recordsTotal = query.Count();

            var result = query.Skip(skip).Take(pageSize).ToList();


            return result;
        }

        /// <summary>
        /// get Affiliate Customers
        /// </summary>
        /// <param name="draw"></param>
        /// <param name="length"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortColumnDirection"></param>
        /// <param name="searchValue"></param>
        /// <param name="pageSize"></param>
        /// <param name="skip"></param>
        /// <param name="recordsTotal"></param>
        /// <param name="userEmail"></param>
        /// <returns>Only Email</returns>
        public List<string> AffiliateCustomers(string draw,
           string length,
           string sortColumn,
           string sortColumnDirection,
           string searchValue,
           int pageSize,
           int skip,
           ref int recordsTotal,
           string userEmail)
        {
            IQueryable<Tb_Sell> query = _dbset;


            var affiliatCode = _context.Tb_Affiliates.FirstOrDefault(d => d.Email == userEmail);

            if (affiliatCode == null)
                return null;

            query = query.Where(d => d.AffiliateCode == affiliatCode.Code);

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                if (sortColumnDirection == "asc")
                {
                    switch (sortColumn)
                    {
                        case "Email":
                            query = query.OrderBy(d => d.Email);
                            break;                        
                        default:
                            break;
                    }
                }
                if (sortColumnDirection == "desc")
                {
                    switch (sortColumn)
                    {
                        case "Email":
                            query = query.OrderByDescending(d => d.Email);
                            break;                        
                        default:
                            break;
                    }
                }
            }


            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(m => m.Email == searchValue);
            }

            

            var result = query.Skip(skip).Take(pageSize).Select(d => d.Email).Distinct().ToList();

            recordsTotal = result.Count();


            return result;
        }

        /// <summary>
        /// get Dashboard Values
        /// </summary>
        /// <param name="count"></param>
        /// <param name="UserId"></param>
        /// <param name="affiliateCode"></param>
        /// <param name="filterValue"></param>
        /// <returns></returns>
        public IEnumerable<Tb_Sell> GetSellOnDashboard(int count, string UserId,ref string affiliateCode, int filterValue = 0)
        {
            IQueryable<Tb_Sell> query = _dbset;

            var user = _context.Users.FirstOrDefault(d => d.Id == UserId && d.IsActive);

            if (user == null)
                return null;

            var affiliatCode = _context.Tb_Affiliates.FirstOrDefault(d => d.Email == user.Email);

            if (affiliatCode == null)
                return null;

            affiliateCode = affiliatCode.Code;

            if(filterValue != 0)
            {
                filterValue = (-filterValue);
                query = query.Where(d => d.CreateAt > DateTime.Now.AddDays(filterValue));
            }

            query.Where(d => d.AffiliateCode == affiliatCode.Code).Take(count).ToList();

            return query;
        }
    }
}
