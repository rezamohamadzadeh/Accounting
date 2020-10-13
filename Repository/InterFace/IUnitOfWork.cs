using DAL.Models;
using Repository.Repositories;
using System.Threading.Tasks;

namespace Repository.InterFace
{
    public interface IUnitOfWork
    {
        public SellRepository SellRepo { get; }

        public UserRepository UserRepo { get; }

        public AffiliateRepository AffiliateRepo { get; }

        public ProductDetailRepository ProductDetailRepo { get; }


        void BackUpFromDb(DatabaseName databaseName);

        void Save();
        Task SaveAsync();
    }
}
