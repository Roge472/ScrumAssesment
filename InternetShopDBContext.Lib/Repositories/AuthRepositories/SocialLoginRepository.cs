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
    public class SocialLoginRepository : BaseRepository<SocialLogin>, ISocialLoginRepository
    {
        public SocialLoginRepository(DbContext context) : base(context)
        {
        }

        public User GetUserByKey(string key)
        {
            var socialLogin = Entities.Include(e => e.User).ThenInclude(u=>u.Role).SingleOrDefault(e => e.ProviderKey == key);

            if (socialLogin != null) return socialLogin.User;
            return null;

        }
    }
}
