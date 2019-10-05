using BaseModelLibrary.Attributes;
using BaseModelLibrary.IRepositories;
using BaseModelLibrary.Lib.Models.UserModels;

namespace InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories
{
    [CustomRepositoryRegistration]
    public interface IEmailLoginRepository : IBaseRepository<EmailLogin>
    {
        void UpdateIsEmailConfirmed(string email);
        User GetUserByEmail(string email);
        User GetUserWithRoleByEmail(string email);
    }
}

