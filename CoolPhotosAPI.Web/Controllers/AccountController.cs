using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using CoolPhotosAPI.BL;
using CoolPhotosAPI.BL.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CoolPhotosAPI.BL.CoolPhotosConstants;

namespace CoolPhotosAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult SignInGoogle(string redirectUrl = "/")
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("HandleGoogleAuthentication", "Account"),
            };
            properties.Items.Add(REDIRECT_URL_KEY, redirectUrl);

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("[action]")]
        public async Task<IActionResult> HandleGoogleAuthentication()
        {
            AuthenticateResult authResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            IIdentity userIdentity = authResult.Principal.Identity;
            await HttpContext.SignInAsync("MainCookie", new ClaimsPrincipal(userIdentity));

            string userSocNetworkIdentifier = authResult.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (_userService.UserDoesntExist(userSocNetworkIdentifier))
            {
                _userService.CreateUser(authResult.Principal);
            }
            
            string redirectUrl = authResult.Properties.Items[REDIRECT_URL_KEY];
            return Redirect(redirectUrl);
        }

        [Authorize(AuthenticationSchemes = COOL_AUTH_SCHEME)]
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetCurrentUserData()
        {
            var claims = User.Claims
                .Select(c => new { c.Type, c.Value })
                .ToArray();
            return Ok(claims);
        }

        [Route("[action]")]
      //  [Authorize(AuthenticationSchemes = COOL_AUTH_SCHEME)]
        public IActionResult TestAuth()
        {
            return Ok(new { fuckYou = false });
        }
    }
}