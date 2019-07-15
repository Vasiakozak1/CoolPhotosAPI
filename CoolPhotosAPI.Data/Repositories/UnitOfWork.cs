using System;
using CoolPhotosAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoolPhotosAPI.Data.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private DbContext _context;
        private IRepository<CoolAppUser> _userRepo;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            _disposed = false;
        }

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

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
