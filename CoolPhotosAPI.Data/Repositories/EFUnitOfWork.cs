using System;
using CoolPhotosAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoolPhotosAPI.Data.Repositories
{
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CoolDbContext _context;
        private IRepository<CoolAppUser> _userRepo;
        private IRepository<Photo> _photoRepo;
        private IRepository<Album> _albumRepo;
        private IRepository<Comment> _commentRepo;

        public EFUnitOfWork(CoolDbContext context) => _context = context;

        public IRepository<CoolAppUser> UserRepo
        {
            get
            {
                if(_userRepo == null)
                {
                    _userRepo = new CoolRepository<CoolAppUser>(_context);
                }
                return _userRepo;
            }
        }

        public IRepository<Photo> PhotoRepo
        {
            get
            {
                if (_photoRepo == null)
                {
                    _photoRepo = new CoolRepository<Photo>(_context);
                }
                return _photoRepo;
            }
        }

        public IRepository<Album> AlbumRepo
        {
            get
            {
                if (_albumRepo == null)
                {
                    _albumRepo = new CoolRepository<Album>(_context);
                }
                return _albumRepo;
            }
        }

        public IRepository<Comment> CommentRepo
        {
            get
            {
                if (_commentRepo == null)
                {
                    _commentRepo = new CoolRepository<Comment>(_context);
                }
                return _commentRepo;
            }
        }

        public void Dispose()
        {
            _context.Database.CloseConnection();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
