using DAL;
using DAL.Models;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repositories
{
    public class UserRepository : GenericRepositori<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext Db) : base(Db)
        { }
        public ApplicationUser GetUserByName(string userName)
        {
            return Get(q => q.UserName == userName).FirstOrDefault();
        }
        public ApplicationUser GetUserIncludeBusinessOwnerById(string id)
        {
            return Get(q => q.Id == id,null, "BusinessOwner").FirstOrDefault();
        }
        public bool CheckUserEmail(string userName,string id)
        {
            var user = Get(q => q.UserName == userName && q.Id != id).FirstOrDefault();
            if (user != null)
                return true;            
            return false;
        }


    }
}
