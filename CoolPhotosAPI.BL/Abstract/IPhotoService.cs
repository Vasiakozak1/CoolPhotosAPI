using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoolPhotosAPI.BL.Abstract
{
    public interface IPhotoService
    {
        Task UploadPhotoAsync(string socNetworkId, IEnumerator<IFormFile> formFiles);
    }
}
