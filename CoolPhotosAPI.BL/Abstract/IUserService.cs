using CoolPhotosAPI.Data.Abstract;
using System.Security.Claims;

namespace CoolPhotosAPI.BL.Abstract
{
    public interface IUserService: IInjectableService
    {
        bool UserDoesntExist(string socNetworkId);
        void CreateUser(ClaimsPrincipal identityUser);
    }
}
