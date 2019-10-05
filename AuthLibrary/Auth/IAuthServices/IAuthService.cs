using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Attributes;

namespace AuthLibrary.Auth.IAuthServices
{
    [CustomRepositoryRegistration]
    public interface IAuthService
    {
        User Authenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload);
    }
}