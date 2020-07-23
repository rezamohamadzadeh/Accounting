using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.InterFace;
using Repository.Repositories;
using System;
using System.Collections.Generic;
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
        public bool BackUpFromDb(string path)
        {
            try
            {
                _dbContext.Database.ExecuteSqlRaw("BACKUP DATABASE BaseProj TO DISK = {0}", path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        
    }
}
