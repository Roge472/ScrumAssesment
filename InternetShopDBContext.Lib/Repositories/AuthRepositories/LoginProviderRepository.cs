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
    public class LoginProviderRepository : BaseRepository<LoginProvider>, ILoginProviderRepository
    {
        public LoginProviderRepository(DbContext context) : base(context)
        {
        }

        public LoginProvider GetGoogleProvider()
        {
            return Entities.SingleOrDefault(e => e.Name == "Google");
        }
        public LoginProvider GetVkProvider()
        {
            return Entities.SingleOrDefault(e => e.Name == "Vk");
        }
    }
}
