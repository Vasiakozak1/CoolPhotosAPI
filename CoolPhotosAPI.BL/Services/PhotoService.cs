using System;
using System.Collections.Generic;
using CoolPhotosAPI.BL.Abstract;
using CoolPhotosAPI.BL.Exceptions;
using CoolPhotosAPI.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using CoolPhotosAPI.Data.Entities;

namespace CoolPhotosAPI.BL.Services
{
    public class PhotoService: IPhotoService
    {
        private const string MEDIA_FOLDER_NAME = "Media";
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _mediaDirectoryPath;
        private string _userMediaDirectoryPath;

        public PhotoService(IHostingEnvironment hostingEnvironment, IUnitOfWork unitOfWork)
        {
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
            _mediaDirectoryPath = Path.Combine(_hostingEnvironment.ContentRootPath, MEDIA_FOLDER_NAME);
        }

        public async Task UploadPhotoAsync(string socNetworkId, IEnumerator<IFormFile> formFiles)
        {
            _userMediaDirectoryPath = Path.Combine(_mediaDirectoryPath, socNetworkId);
            using (formFiles)
            {
                EnsureFolderForUserFilesCreated(socNetworkId);
                Guid userGuid = _unitOfWork.UserRepo
                    .GetSingle(u => u.SocNetworkId.Equals(socNetworkId)).Guid;
                await UploadPhotoAsync(userGuid, formFiles);
                _unitOfWork.SaveChanges();
            }
        }

        private void EnsureFolderForUserFilesCreated(string userSocNetworkId)
        {
            if (!Directory.Exists(_mediaDirectoryPath))
            {
                Directory.CreateDirectory(_mediaDirectoryPath);
            }

            if (!Directory.Exists(_userMediaDirectoryPath))
            {
                Directory.CreateDirectory(_userMediaDirectoryPath);
            }
        }

        private async Task UploadPhotoAsync(Guid userGuid, IEnumerator<IFormFile> formFiles)
        {
            if (formFiles.MoveNext())
            {
                IFormFile file = formFiles.Current;
                EnsureFileIsValid(file.ContentType);
                string uploadingFilePath = Path.Combine(_userMediaDirectoryPath, file.FileName);
                EnsureFileDoesntExist(file.FileName);

                using (var fileStream = new FileStream(uploadingFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var uploadedPhoto = new Photo
                {
                    DateCreated = DateTime.Now,
                    Guid = Guid.NewGuid(),
                    OwnerGuid = userGuid,
                    Path = uploadingFilePath
                };
                _unitOfWork.PhotoRepo.Create(uploadedPhoto);

                await UploadPhotoAsync(userGuid, formFiles);
            }
        }

        private void EnsureFileDoesntExist(string filePath)
        {
            if (File.Exists(filePath))
            {
                throw new FileAlreadyExistsException(filePath);
            }
        }

        private void EnsureFileIsValid(string fileContentType)
        {
            if (!(string.Equals(fileContentType, "image/jpg", StringComparison.OrdinalIgnoreCase)
               || string.Equals(fileContentType, "image/png", StringComparison.OrdinalIgnoreCase)
               || string.Equals(fileContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase))
               )
            {
                throw new FileFormatException();
            }
        }
    }
}
