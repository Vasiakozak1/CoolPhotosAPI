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

            modelBuilder.Entity("CoolPhotosAPI.Data.Entities.CoolAppUser", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("SocNetworkId");

                    b.HasKey("Guid");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
