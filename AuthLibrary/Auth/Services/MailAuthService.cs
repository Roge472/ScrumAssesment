using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories;
using BaseModelLibrary.Lib.Models.UserModels;
using Google.Apis.Auth;
using AuthLibrary.Auth.IAuthServices;
using AuthLibrary.Registration;
using BaseModelLibrary.HelperModels.TrimmedModles.UserModels;
using AuthLibrary.Auth.EmailRegistration;

namespace AuthLibrary.Auth.Services
{
    public class MailAuthService : IEmailAuth
    {
        private IUserRepository userRepository;
        private IRoleRepository roleRepository;
        private IEmailLoginRepository emailLoginRepository;

        public MailAuthService(IUserRepository userRepository, IEmailLoginRepository emailLoginRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.emailLoginRepository = emailLoginRepository;
        }
        public User MailAuthenticate(string email, string password)
        {
            var user= this.FindUser(email, password);
            if (PasswordController.CheckUserPassword(user, password))
            {
                return user;
            }
            return null;
        }

        private User FindUser(string email, string password)
        {
            var user = emailLoginRepository.GetUserWithRoleByEmail(email);
            return user;
        }


        public bool ConfirmEmail(string email, string hash)
        {
            if (JwtController.ValidateToken(hash, email))
            {
                emailLoginRepository.UpdateIsEmailConfirmed(email);
                return true;
            }
           return false;
        }

        public bool MailRegistrate(TrimmedEmailUser trimmedEmailUser)
        {
            var email = trimmedEmailUser.Email;
            var password = trimmedEmailUser.Password;

            var user = emailLoginRepository.GetUserByEmail(email);
            if (user == null)
            {
                EmailLogin emailLogin = new EmailLogin();
                emailLogin.Email = email;
                PasswordController.CreatePassword(emailLogin, password);

                user = CreateUserByEmailLogin(trimmedEmailUser);
                userRepository.Add(user);
                emailLogin.User = user;
                emailLoginRepository.Add(emailLogin);
            }
            return true;
        }
        public User CreateUserByEmailLogin(TrimmedEmailUser trimmedEmailUser)
        {
            var user = new User()
            {
                Name = trimmedEmailUser.FirstName,
                LastName = trimmedEmailUser.LastName,
                RegistrationDate = DateTime.Now,
                Role = roleRepository.GetUserRole()
            };
            return user;
        }
    }
}