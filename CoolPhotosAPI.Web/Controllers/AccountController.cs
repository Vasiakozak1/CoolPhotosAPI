using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using CoolPhotosAPI.BL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolPhotosAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public IActionResult SignInGoogle(string redirectUrl = "/")
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("HandleGoogleAuthentication", "Account"),
            };
            properties.Items.Add(CoolPhotosConstants.REDIRECT_URL_KEY, redirectUrl);

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("[action]")]
        public async Task<IActionResult> HandleGoogleAuthentication()
        {
            AuthenticateResult authResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            IIdentity userIdentity = authResult.Principal.Identity;
            await HttpContext.SignInAsync("MainCookie", new ClaimsPrincipal(userIdentity));
            
            string redirectUrl = authResult.Properties.Items[CoolPhotosConstants.REDIRECT_URL_KEY];
            return Redirect(redirectUrl);
        }
        
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "MainCookie")]
        public IActionResult TestAuth()
        {
            return Ok(new { fuckYou = false });
        }
    }
}