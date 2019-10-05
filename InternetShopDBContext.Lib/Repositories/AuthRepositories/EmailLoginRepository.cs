using BaseModelLibrary.Lib.Models.UserModels;
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
    public class EmailLoginRepository : BaseRepository<EmailLogin>, IEmailLoginRepository
    {
        public EmailLoginRepository(DbContext context) : base(context)
        {
        }

        public User GetUserWithRoleByEmail(string email)
        {
            var emailLogin=Entities.Include(e => e.User).ThenInclude(u=>u.Role).SingleOrDefault(e => e.Email == email);
            if (emailLogin == null) return null;
            var user = emailLogin.User;
            return user;
        }

        public User GetUserByEmail(string email)
        {
            // check include
            var emailLogin=Entities.Include(e=>e.User).SingleOrDefault(e => e.Email == email);
            if (emailLogin == null) return null;
            var user = emailLogin.User;
            return user;
        }

        public void UpdateIsEmailConfirmed(string email)
        {
            var emailLogin = Entities.FirstOrDefault(e => e.Email == email);
            emailLogin.IsEmailConfirmed = 1;
            Context.SaveChanges();
        }
    }
}
