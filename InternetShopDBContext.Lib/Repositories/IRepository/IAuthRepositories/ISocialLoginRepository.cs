using BaseModelLibrary.Attributes;
using BaseModelLibrary.IRepositories;
using BaseModelLibrary.Lib.Models.UserModels;

namespace InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories
{
    [CustomRepositoryRegistration]
    public interface ISocialLoginRepository: IBaseRepository<SocialLogin>
    {
        User GetUserByKey(string key);
    }
}

