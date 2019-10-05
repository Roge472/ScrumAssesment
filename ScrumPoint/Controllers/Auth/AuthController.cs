using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Google.Apis.Auth;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthLibrary.Auth.Models;
using AuthLibrary.Auth.IAuthServices;
using AuthLibrary.Auth.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using BaseModelLibrary.Lib.Models.UserModels;
using Microsoft.AspNetCore.Authentication;
using System.Net;
using System.Net.Mail;
using InternetShop.ControllersHelper;
using BaseModelLibrary.HelperModels.TrimmedModles.UserModels;
using System.IO;
using BaseModelLibrary.Models.UserModels;

namespace InternetShop.Controllers.Auth
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private IGoogleAuth _googleAuth;
        private IEmailAuth _emailAuth;

        public AuthController(IGoogleAuth googleAuth, IEmailAuth emailAuth)
        {
            this._googleAuth = googleAuth;
            this._emailAuth = emailAuth;
        }

        [HttpPost("[action]")]
        public string SendEmail(string email, string password)
        {
            return EmailSender.SendMail(email, password);
        }

        [HttpPost("[action]")]
        public ActionResult EmailLogin(string email, string password)
        {
            var user = _emailAuth.MailAuthenticate(email, password);
            if (user != null && user.EmailLogin.IsEmailConfirmed==1)
            {
                User sendedUser = new User() { Id = user.Id, Role = new Role() { Id = user.Role.Id, Name = user.Role.Name } };

                // update cookie (add if role changed only)
                Cookie(user);
                return Json(sendedUser);
            }
            return Json(null);
        }

        [HttpGet("[action]")]
        public RedirectResult EmailConfirmation(string email, string hash)
        {
             _emailAuth.ConfirmEmail(email, hash);
            return Redirect("/login");
        }

        [HttpPost("[action]")]
        public bool EmailRegistration([FromBody]TrimmedEmailUser trimmedEmailUser)
        {

            var isRegistrated = _emailAuth.MailRegistrate(trimmedEmailUser);

            if (isRegistrated)
            {
                EmailSender.SendAuthLetter(trimmedEmailUser.Email, "https://localhost:44347/api/Auth/EmailConfirmation");
                return true;
            }
            return false;
        }

        [HttpPost("[action]")]
        public ActionResult GoogleLogin([FromBody]UserView userView)
        {
            var user = _googleAuth.GoogleLogin(userView);
            if (user != null)
            {
                User sendedUser = new User() { Id = user.Id, Role = new Role() { Id = user.Role.Id, Name = user.Role.Name } };
                Cookie(user);

                return Json(sendedUser);
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
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        [HttpPost("[action]")]
        public void Logout()
        {
            CookieLogout();
        }
        private async void CookieLogout()
        {
            await HttpContext.SignOutAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }

}

/*SimpleLogger.Log(payload.Name);
                SimpleLogger.Log(payload.Email);
                SimpleLogger.Log(payload.Subject);
                SimpleLogger.Log(payload.Issuer);
                SimpleLogger.Log(payload.ExpirationTimeSeconds.ToString());

                SimpleLogger.Log(DateUtil.FromUnixTime((long)payload.ExpirationTimeSeconds).ToString());
                SimpleLogger.Log(payload.JwtId);*/
