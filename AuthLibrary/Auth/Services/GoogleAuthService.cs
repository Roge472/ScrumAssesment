using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using BaseModelLibrary.Lib.Models.UserModels;
using Google.Apis.Auth;
using AuthLibrary.Auth.Helpers;
using InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories;
using AuthLibrary.Auth.IAuthServices;
using AuthLibrary.Auth.Models;
using BaseModelLibrary.Models.UserModels;

namespace AuthLibrary.Auth.Services
{
    public class GoogleAuthService : IGoogleAuth
    {
        private IUserRepository userRepository;
        private IRoleRepository roleRepository;
        private ISocialLoginRepository socialLoginRepository;
        private ILoginProviderRepository loginProviderRepository;

        public GoogleAuthService(IUserRepository userRepository, ISocialLoginRepository socialLoginRepository, ILoginProviderRepository loginProviderRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.socialLoginRepository = socialLoginRepository;
            this.loginProviderRepository = loginProviderRepository;
            this.roleRepository = roleRepository;
        }

        public User GoogleLogin(UserView userView)
        {
            try
            {
                //SimpleLogger.Log("userView = " + userView.tokenId);
                var payload = GoogleJsonWebSignature.ValidateAsync(userView.tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;
                var user = GoogleAuthenticate(payload);


                return user;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public User GoogleAuthenticate(GoogleJsonWebSignature.Payload payload)
        {
            return this.FindUserOrAdd(payload);
        }

        private User FindUserOrAdd(GoogleJsonWebSignature.Payload payload)
        {

            var user = socialLoginRepository.GetUserByKey(payload.Subject);
            if (user == null)
            {
                SocialLogin socialLogin = new SocialLogin();
                socialLogin.ProviderKey = payload.Subject;
                socialLogin.Login = payload.Email;
                socialLogin.LoginProvider = loginProviderRepository.GetGoogleProvider();


                user = CreateUserByGooglePayload(payload);
                user.Role = new Role() { Id = 1 };
                userRepository.Add(user);
                socialLogin.User = user;
                socialLoginRepository.Add(socialLogin);

               

            }
            return user;
        }
        public User CreateUserByGooglePayload(GoogleJsonWebSignature.Payload payload)
        {
            var user = new User()
            {
                Name = payload.Name,
                LastName = payload.GivenName,
                RegistrationDate = DateTime.Now,
                Role = roleRepository.GetUserRole()
            };
            return user;
        }
       
    }
}