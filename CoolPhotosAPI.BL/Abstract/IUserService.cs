using System.Security.Claims;

namespace CoolPhotosAPI.BL.Abstract
{
    public interface IUserService
    {
        bool UserDoesntExist(string socNetworkId);
        void CreateUser(ClaimsPrincipal identityUser);
    }
}
