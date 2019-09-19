using CoolPhotosAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoolPhotosAPI.Data
{
    public class CoolDbContext: DbContext
    {
        public CoolDbContext(DbContextOptions options) : base(options) { }

        public DbSet<CoolAppUser> Users { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoolAppUser>()
                .HasKey(user => user.Guid);
            modelBuilder.Entity<CoolAppUser>()
                .HasIndex(user => user.Email)
                .IsUnique();
            modelBuilder.Entity<CoolAppUser>()
                .HasIndex(user => user.SocNetworkId)
                .IsUnique();

            modelBuilder.Entity<PhotosAlbumsConnection>()
                .HasKey(pa => new { pa.PhotoGuid, pa.AlbumGuid });
            modelBuilder.Entity<PhotosAlbumsConnection>()
                .HasOne(pa => pa.Album)
                .WithMany(al => al.Photos)
                .HasForeignKey(pa => pa.AlbumGuid)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PhotosAlbumsConnection>()
                .HasOne(pa => pa.Photo)
                .WithMany(ph => ph.Albums)
                .HasForeignKey(pa => pa.PhotoGuid)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Photo>()
                .HasKey(ph => ph.Guid)
                .ForSqlServerIsClustered();
            modelBuilder.Entity<Photo>()
                .HasMany(p => p.Albums)
                .WithOne(al => al.Photo);

            modelBuilder.Entity<Photo>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Photos)
                .HasForeignKey(p => p.OwnerGuid);

            modelBuilder.Entity<Album>()
                .HasKey(a => a.Guid)
                .ForSqlServerIsClustered();
            modelBuilder.Entity<Album>()
                .HasOne(a => a.Owner)
                .WithMany(u => u.Albums)
                .HasForeignKey(a => a.OwnerGuid);
        

            modelBuilder.Entity<Comment>()
                .HasKey(c => c.Guid);
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Album)
                .WithMany(al => al.Comments)
                .HasForeignKey(c => c.AlbumGuid);

            base.OnModelCreating(modelBuilder);
        }
    }
}
