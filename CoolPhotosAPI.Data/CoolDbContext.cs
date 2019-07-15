using CoolPhotosAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoolPhotosAPI.Data
{
    public class CoolDbContext: DbContext
    {
        public CoolDbContext(DbContextOptions options) : base(options) { }

        public DbSet<CoolAppUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoolAppUser>()
                .HasKey(user => user.Guid);
            modelBuilder.Entity<CoolAppUser>()
                .Property(user => user.Email);

            base.OnModelCreating(modelBuilder);
        }
    }
}
