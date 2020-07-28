using DAL.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.InterFace
{
    public interface IUnitOfWork
    {
        public SellRepository SellRepo { get; }

        public UserRepository UserRepo { get; }

        public AffiliateRepository AffiliateRepo { get; }

        void BackUpFromDb(DatabaseName databaseName);

        void Save();
        Task SaveAsync();
    }
}
