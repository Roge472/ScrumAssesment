using BaseModelLibrary.Lib.Models;
using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Models.UserModels;
using BaseModelLibrary.Repositories;
using InternetShopDBContext.Lib.Contexts;
using InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternetShopDBContext.Lib.Repositories.UserRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public User FindByEmail(string email)
        {
            var user=Entities.SingleOrDefault(e => e.EmailLogin.Email == email);
            return user;
        }

        public Role GetUserRole(int userId)
        {
            var user= Entities.Include(u=>u.Role).SingleOrDefault(e => e.Id==userId);
            return user.Role;
        }
    }
}
