using BaseModelLibrary.Attributes;
using BaseModelLibrary.IRepositories;
using BaseModelLibrary.Lib.Models.UserModels;

namespace InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories
{
    [CustomRepositoryRegistration]
    public interface ILoginProviderRepository : IBaseRepository<LoginProvider>
    {
        LoginProvider GetGoogleProvider();
        LoginProvider GetVkProvider();
    }
}

