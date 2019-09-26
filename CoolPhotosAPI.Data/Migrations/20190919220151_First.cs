using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoolPhotosAPI.Data.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SocNetworkId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    OwnerGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Guid)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Albums_Users_OwnerGuid",
                        column: x => x.OwnerGuid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    OwnerGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Guid)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Photos_Users_OwnerGuid",
                        column: x => x.OwnerGuid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    AlbumGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Comments_Albums_AlbumGuid",
                        column: x => x.AlbumGuid,
                        principalTable: "Albums",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotosAlbumsConnection",
                columns: table => new
                {
                    PhotoGuid = table.Column<Guid>(nullable: false),
                    AlbumGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosAlbumsConnection", x => new { x.PhotoGuid, x.AlbumGuid });
                    table.ForeignKey(
                        name: "FK_PhotosAlbumsConnection_Albums_AlbumGuid",
                        column: x => x.AlbumGuid,
                        principalTable: "Albums",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotosAlbumsConnection_Photos_PhotoGuid",
                        column: x => x.PhotoGuid,
                        principalTable: "Photos",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_OwnerGuid",
                table: "Albums",
                column: "OwnerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AlbumGuid",
                table: "Comments",
                column: "AlbumGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_OwnerGuid",
                table: "Photos",
                column: "OwnerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosAlbumsConnection_AlbumGuid",
                table: "PhotosAlbumsConnection",
                column: "AlbumGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SocNetworkId",
                table: "Users",
                column: "SocNetworkId",
                unique: true,
                filter: "[SocNetworkId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "PhotosAlbumsConnection");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
