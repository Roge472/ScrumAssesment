using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories;
using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Attributes;
using AuthLibrary.Auth.Models;

namespace AuthLibrary.Auth.IAuthServices
{
    [CustomRepositoryRegistration]
    public interface IGoogleAuth
    {
        User GoogleAuthenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload);
        User GoogleLogin(UserView userView);
    }
}