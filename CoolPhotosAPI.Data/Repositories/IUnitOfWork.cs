using CoolPhotosAPI.Data.Entities;
using System;

namespace CoolPhotosAPI.Data.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<CoolAppUser> UserRepo { get; }

        void SaveChanges();
    }
}
