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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }

        public Role GetAdminRole()
        {
            return Entities.SingleOrDefault(r => r.Name == "Admin");
        }

        public Role GetModeratorRole()
        {
            return Entities.SingleOrDefault(r => r.Name == "Moderator");
        }

        public Role GetUserRole()
        {
            return Entities.SingleOrDefault(r => r.Name == "User");
        }
    }
}
