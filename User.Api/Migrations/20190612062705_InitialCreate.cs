using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBPFiles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserID = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    OriginFilePath = table.Column<string>(nullable: true),
                    FormatFilePath = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBPFiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserName = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Gender = table.Column<byte>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    ProvinceID = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    CityID = table.Column<int>(nullable: false),
                    NameCard = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserTags",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    Tag = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTags", x => new { x.UserID, x.Tag });
                });

            migrationBuilder.CreateTable(
                name: "UserPorperty",
                columns: table => new
                {
                    AppUserID = table.Column<int>(nullable: false),
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(maxLength: 100, nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPorperty", x => new { x.Key, x.AppUserID, x.Value });
                    table.ForeignKey(
                        name: "FK_UserPorperty_Users_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPorperty_AppUserID",
                table: "UserPorperty",
                column: "AppUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBPFiles");

            migrationBuilder.DropTable(
                name: "UserPorperty");

            migrationBuilder.DropTable(
                name: "UserTags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
