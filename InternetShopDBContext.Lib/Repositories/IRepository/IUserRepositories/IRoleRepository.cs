using BaseModelLibrary.Attributes;
using BaseModelLibrary.IRepositories;
using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Models.UserModels;

namespace InternetShopDBContext.Lib.Repositories.IRepository.IUserRepositories
{
    [CustomRepositoryRegistration]
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Role GetAdminRole();
        Role GetModeratorRole();
        Role GetUserRole();
   
    }
}
