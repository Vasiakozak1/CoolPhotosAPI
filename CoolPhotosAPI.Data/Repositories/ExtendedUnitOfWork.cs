using System;
using CoolPhotosAPI.Data.Entities;

namespace CoolPhotosAPI.Data.Repositories
{
    public class ExtendedUnitOfWork : IUnitOfWork
    {
        private readonly CoolDbContext _dbContext;
        private ExtendedPhotoRepository _photoRepo;

        public ExtendedUnitOfWork(CoolDbContext dbContext) => _dbContext = dbContext;

        public IRepository<CoolAppUser> UserRepo => throw new NotImplementedException();

        public IRepository<Photo> PhotoRepo
        {
            get
            {
                if(_photoRepo == null)
                {
                    _photoRepo = new ExtendedPhotoRepository(_dbContext);
                }

                return _photoRepo;
            }
        }

        public IRepository<Album> AlbumRepo => throw new NotImplementedException();

        public IRepository<Comment> CommentRepo => throw new NotImplementedException();

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
