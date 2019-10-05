using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseModelLibrary.Lib.Models;
using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Models.CloudModels;
using BaseModelLibrary.Models.UserModels;
using InternetShopDBContext.Lib.Contexts;
using InternetShopDBContext.Lib.Repositories.IRepository.ICloudRepositories;
using InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("[action]")]
        public string GetUserId()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var idClaim = claims.SingleOrDefault(claim => claim.Type == "Id");

            if (idClaim != null) return idClaim.Value;
            return "-1";
        }

        // Change to init user
        [HttpGet("[action]")]
        public ActionResult GetUser()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var idClaim = claims.SingleOrDefault(claim => claim.Type == "Id");

            if (idClaim != null)
            {
                var id = Int32.Parse(idClaim.Value);
                var role = userRepository.GetUserRole(id);
                var user = new User() { Id = id, Role = new Role() { Name = role.Name } };

                // update cookie if role changed
                Cookie(user);

                return Json(user);
            }
            return Json(null);
        }
        private async void Cookie(User user)
        {
            if (user == null || user.Role == null || user.Role.Name == null) return;
            var claims = new List<Claim>
                              {
                                  new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                  new Claim("Id", user.Id.ToString()),
                                  new Claim(ClaimTypes.Role, user.Role.Name),
                              };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
