using CoolPhotosAPI.BL.ViewModels;
using CoolPhotosAPI.Data.Abstract;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoolPhotosAPI.BL.Abstract
{
    public interface IPhotoService: IInjectableService
    {
        Task UploadPhotoAsync(string socNetworkId, IEnumerator<IFormFile> formFiles);
        Task<ICollection<PhotoViewModel>> GetAllPhotosAsync(string socNetworkId);
    }
}
