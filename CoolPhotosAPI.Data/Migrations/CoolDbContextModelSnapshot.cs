﻿// <auto-generated />
using System;
using CoolPhotosAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoolPhotosAPI.Data.Migrations
{
    [DbContext(typeof(CoolDbContext))]
    partial class CoolDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoolPhotosAPI.Data.Entities.Album", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<Guid>("OwnerGuid");

                    b.HasKey("Guid")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("OwnerGuid");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("CoolPhotosAPI.Data.Entities.Comment", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AlbumGuid");

                    b.Property<string>("Text");

                    b.HasKey("Guid");

                    b.HasIndex("AlbumGuid");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CoolPhotosAPI.Data.Entities.CoolAppUser", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("SocNetworkId");

                    b.HasKey("Guid");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("SocNetworkId")
                        .IsUnique()
                        .HasFilter("[SocNetworkId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoolPhotosAPI.Data.Entities.Photo", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<Guid>("OwnerGuid");

                    b.Property<string>("Path");

                    b.HasKey("Guid")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("OwnerGuid");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("CoolPhotosAPI.Data.Entities.PhotosAlbumsConnection", b =>
                {
                    b.Property<Guid>("PhotoGuid");

                    b.Property<Guid>("AlbumGuid");

                    b.HasKey("PhotoGuid", "AlbumGuid");

                    b.HasIndex("AlbumGuid");

                    b.ToTable("PhotosAlbumsConnection");
                });

            modelBuilder.Entity("CoolPhotosAPI.Data.Entities.Album", b =>
                {
                    b.HasOne("CoolPhotosAPI.Data.Entities.CoolAppUser", "Owner")
                        .WithMany("Albums")
                        .HasForeignKey("OwnerGuid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoolPhotosAPI.Data.Entities.Comment", b =>
                {
                    b.HasOne("CoolPhotosAPI.Data.Entities.Album", "Album")
                        .WithMany("Comments")
                        .HasForeignKey("AlbumGuid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoolPhotosAPI.Data.Entities.Photo", b =>
                {
                    b.HasOne("CoolPhotosAPI.Data.Entities.CoolAppUser", "Owner")
                        .WithMany("Photos")
                        .HasForeignKey("OwnerGuid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoolPhotosAPI.Data.Entities.PhotosAlbumsConnection", b =>
                {
                    b.HasOne("CoolPhotosAPI.Data.Entities.Album", "Album")
                        .WithMany("Photos")
                        .HasForeignKey("AlbumGuid")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CoolPhotosAPI.Data.Entities.Photo", "Photo")
                        .WithMany("Albums")
                        .HasForeignKey("PhotoGuid")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
