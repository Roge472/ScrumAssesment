using BaseModelLibrary.Attributes;
using BaseModelLibrary.IRepositories;
using BaseModelLibrary.Lib.Models;
using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Models.UserModels;

namespace InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories
{
    [CustomRepositoryRegistration]
    public interface IUserRepository : IBaseRepository<User>
    {
        User FindByEmail(string email);
        Role GetUserRole(int userId);
    }
}
