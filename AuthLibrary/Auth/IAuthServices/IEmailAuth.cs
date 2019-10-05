using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories;
using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Attributes;
using BaseModelLibrary.HelperModels.TrimmedModles.UserModels;

namespace AuthLibrary.Auth.IAuthServices
{
    [CustomRepositoryRegistration]
    public interface IEmailAuth
    {
        User MailAuthenticate(string email, string password);
        bool MailRegistrate(TrimmedEmailUser trimmedEmailUser);
        bool ConfirmEmail(string email, string hash);
    }
}