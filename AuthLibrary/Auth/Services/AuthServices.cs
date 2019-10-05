using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories;
using BaseModelLibrary.Lib.Models.UserModels;
using AuthLibrary.Auth.IAuthServices;

namespace AuthLibrary.Auth.Services
{
    public class AuthServices : IAuthService
    {
        private IGoogleAuth googleAuth;

        public AuthServices(IGoogleAuth googleAuth)
        {
            this.googleAuth = googleAuth;
        }
        public User Authenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
        {
            return googleAuth.GoogleAuthenticate(payload);
        }

      
       
    }
}