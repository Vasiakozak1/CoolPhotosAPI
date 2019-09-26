using CoolPhotosAPI.Data.Entities;
using System;

namespace CoolPhotosAPI.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<CoolAppUser> UserRepo { get; }

        IRepository<Photo> PhotoRepo { get; }

        IRepository<Album> AlbumRepo { get; }

        IRepository<Comment> CommentRepo { get; }

        void SaveChanges();
    }
}
