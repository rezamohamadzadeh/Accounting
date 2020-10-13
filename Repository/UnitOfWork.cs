using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.InterFace;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private UserRepository _UserRepo;
        public UserRepository UserRepo
        {
            get
            {
                if (_UserRepo == null)
                {
                    _UserRepo = new UserRepository(_dbContext);
                }
                return _UserRepo;
            }
        }



        private AffiliateRepository _affiliateRepository;

        public AffiliateRepository AffiliateRepo
        {
            get
            {
                if (_affiliateRepository == null)
                {
                    _affiliateRepository = new AffiliateRepository(_dbContext);
                }
                return _affiliateRepository;
            }
        }

        private SellRepository _sellRepository;

        public SellRepository SellRepo
        {
            get
            {
                if (_sellRepository == null)
                {
                    _sellRepository = new SellRepository(_dbContext);
                }
                return _sellRepository;
            }
        }
        private ProductDetailRepository _productDetailRepository;

        public ProductDetailRepository ProductDetailRepo
        {
            get
            {
                if (_productDetailRepository == null)
                {
                    _productDetailRepository = new ProductDetailRepository(_dbContext);
                }
                return _productDetailRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }


        #region BackUpFromDb

        /// <summary>
        /// Back Up from db with hangfire jobs
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public void BackUpFromDb(DatabaseName databaseName)
        {
            string savePath = "";
            string DirectoryPath = "";
            string BackUpFolder = "BackUp\\";
            var dbName = databaseName.ToString();

            switch (databaseName)
            {
                case DatabaseName.BaseProj:
                    savePath = Path.Combine(Directory.GetCurrentDirectory(), BackUpFolder + dbName + "\\", dbName + "_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".bak");
                    DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), BackUpFolder + dbName);

                    break;
                case DatabaseName.BuyerProfile:
                    savePath = Path.Combine(Directory.GetCurrentDirectory(), BackUpFolder + dbName + "\\", dbName + "_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".bak");
                    DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), BackUpFolder + dbName);

                    break;
                default:
                    break;
            }
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            _dbContext.Database.ExecuteSqlRaw("BACKUP DATABASE {0} TO DISK = {1}", dbName, savePath);

        }
        #endregion

    }
}
