using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CoolPhotosAPI.BL.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CoolPhotosAPI.BL.CoolPhotosConstants;

namespace CoolPhotosAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : Controller
    {
        private readonly IPhotoService _photoService;

        public PhotosController(
            IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [Authorize(AuthenticationSchemes = COOL_AUTH_SCHEME)]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UploadPhotos([FromForm] IList<IFormFile> images)
        {
            string userSocnetworkId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _photoService.UploadPhotoAsync(userSocnetworkId, images.GetEnumerator());
            return Ok();
        }
    }
}