using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class Initial_DataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProvinceName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvinceID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleTitle = table.Column<string>(maxLength: 200, nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RoleDateTime = table.Column<DateTime>(nullable: false),
                    RoleActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "StoreTypes",
                columns: table => new
                {
                    StoreTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StoreTypeTitle = table.Column<string>(maxLength: 200, nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    StoreTypeDateTime = table.Column<DateTime>(nullable: false),
                    StoreTypeEditeDateTime = table.Column<DateTime>(nullable: false),
                    UserEditorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreTypes", x => x.StoreTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserFristName = table.Column<string>(maxLength: 200, nullable: true),
                    UserLastName = table.Column<string>(maxLength: 300, nullable: true),
                    UserName = table.Column<string>(maxLength: 200, nullable: true),
                    UserEmail = table.Column<string>(maxLength: 300, nullable: false),
                    UserPassword = table.Column<string>(maxLength: 200, nullable: false),
                    UserEmailConfigurationCode = table.Column<string>(nullable: true),
                    UserEmailConfigurationDateTime = table.Column<DateTime>(nullable: false),
                    UserBirthday = table.Column<string>(maxLength: 11, nullable: true),
                    UserImage = table.Column<string>(nullable: true),
                    UserAbout = table.Column<string>(maxLength: 500, nullable: true),
                    UserIsActive = table.Column<bool>(nullable: false),
                    UserDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StoreName = table.Column<string>(maxLength: 200, nullable: false),
                    StoreImage = table.Column<string>(maxLength: 100, nullable: false),
                    StoreLogo = table.Column<string>(maxLength: 100, nullable: false),
                    StoreAbout = table.Column<string>(maxLength: 400, nullable: false),
                    ProvinceID = table.Column<int>(nullable: false),
                    StoreAddress = table.Column<string>(maxLength: 400, nullable: false),
                    StoreIsActive = table.Column<bool>(nullable: false),
                    StoreDateTime = table.Column<DateTime>(nullable: false),
                    StoreEditeDateTime = table.Column<DateTime>(nullable: false),
                    StoreTypeID = table.Column<int>(nullable: false),
                    UserEditorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreID);
                    table.ForeignKey(
                        name: "FK_Stores_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stores_StoreTypes_StoreTypeID",
                        column: x => x.StoreTypeID,
                        principalTable: "StoreTypes",
                        principalColumn: "StoreTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    URID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false),
                    UserRegisterID = table.Column<int>(nullable: false),
                    UserRoleDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.URID);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_ProvinceID",
                table: "Stores",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreTypeID",
                table: "Stores",
                column: "StoreTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleID",
                table: "UserRoles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserID",
                table: "UserRoles",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "StoreTypes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
