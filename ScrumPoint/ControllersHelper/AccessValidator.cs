using BaseModelLibrary.Lib.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetShop.ControllersHelper
{
    public static class AccessValidator
    {
        public static bool IsAllowedAccessToProduct(User productSeller, ClaimsIdentity currentUser)
        {
            IEnumerable<Claim> claims = currentUser.Claims;

            try
            {
                var id = Convert.ToInt32(claims.SingleOrDefault(c => c.Type == "Id").Value);
                if (productSeller.Id == id) return true;
                if (productSeller.Role.Name == "Admin" || productSeller.Role.Name == "Moderator") return true;
            }
            catch 
            {

            }
            return false;
        }
    }
}
