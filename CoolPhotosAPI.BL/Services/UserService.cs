using CoolPhotosAPI.BL.Abstract;
using CoolPhotosAPI.Data.Entities;
using CoolPhotosAPI.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace CoolPhotosAPI.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateUser(ClaimsPrincipal userClaims)
        {
            var user = new CoolAppUser
            {
                Email = userClaims.FindFirst(ClaimTypes.Email).Value,
                FullName = userClaims.FindFirst(ClaimTypes.Name).Value,
                SocNetworkId = userClaims.FindFirst(ClaimTypes.NameIdentifier).Value
            };

            _unitOfWork.UserRepo.Create(user);
            _unitOfWork.SaveChanges();
        }

        public bool UserDoesntExist(string socNetworkId)
        {
            return _unitOfWork.UserRepo
                .GetSingle(u => u.SocNetworkId.Equals(socNetworkId)) == null;
        }
    }
}
