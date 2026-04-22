using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AYMDating.DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BDataSchema");

            migrationBuilder.EnsureSchema(
                name: "ASecurity");

            migrationBuilder.CreateTable(
                name: "ContactUss",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUss", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FinancialModes",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialModes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Purposes",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purposes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendingEmails",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendingEmails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activated = table.Column<bool>(type: "bit", nullable: false),
                    IsStopped = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateOfJoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "ASecurity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivateUsers",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivateUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActivateUsers_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ForgotPasswordUsers",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgotPasswordUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ForgotPasswordUsers_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserBlocks",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBlocks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserBlocks_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBlocks_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavorites",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorites", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserFavorites_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFavorites_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LinkName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLikes",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserLikes_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLikes_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "ASecurity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMessageGroups",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessageGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserMessageGroups_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMessageGroups_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserMessages",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    IsDeletedFromSender = table.Column<bool>(type: "bit", nullable: false),
                    IsDeletedFromReceiver = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserMessages_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMessages_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPackages",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PackageId = table.Column<int>(type: "int", nullable: true),
                    PackageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPackages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserPackages_Packages_PackageId",
                        column: x => x.PackageId,
                        principalSchema: "BDataSchema",
                        principalTable: "Packages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserPackages_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPayments",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPayments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserPayments_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserReports",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReports", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserReports_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserReports_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "ASecurity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "ASecurity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "ASecurity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokenTransactions",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfMaking = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokenTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserTokenTransactions_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserViews",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserViews_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserViews_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserHistories",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    PurposeId = table.Column<int>(type: "int", nullable: true),
                    FinancialModeId = table.Column<int>(type: "int", nullable: true),
                    EducationId = table.Column<int>(type: "int", nullable: true),
                    UserPackageId = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileHeading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutYou = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutPartner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    IsSwitchedOff = table.Column<bool>(type: "bit", nullable: false),
                    SearchAgeFrom = table.Column<int>(type: "int", nullable: true),
                    SearchAgeTo = table.Column<int>(type: "int", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserHistories_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "BDataSchema",
                        principalTable: "Countries",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistories_Educations_EducationId",
                        column: x => x.EducationId,
                        principalSchema: "BDataSchema",
                        principalTable: "Educations",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistories_FinancialModes_FinancialModeId",
                        column: x => x.FinancialModeId,
                        principalSchema: "BDataSchema",
                        principalTable: "FinancialModes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistories_Genders_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "BDataSchema",
                        principalTable: "Genders",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistories_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "BDataSchema",
                        principalTable: "Jobs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistories_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "BDataSchema",
                        principalTable: "Languages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistories_MaritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalSchema: "BDataSchema",
                        principalTable: "MaritalStatuses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistories_Purposes_PurposeId",
                        column: x => x.PurposeId,
                        principalSchema: "BDataSchema",
                        principalTable: "Purposes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistories_UserPackages_UserPackageId",
                        column: x => x.UserPackageId,
                        principalSchema: "BDataSchema",
                        principalTable: "UserPackages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistories_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivateUsers_AppUserId",
                schema: "BDataSchema",
                table: "ActivateUsers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ForgotPasswordUsers_AppUserId",
                schema: "BDataSchema",
                table: "ForgotPasswordUsers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "ASecurity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "ASecurity",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlocks_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserBlocks",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlocks_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserBlocks",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "ASecurity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserFavorites",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserFavorites",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_AppUserId",
                schema: "BDataSchema",
                table: "UserHistories",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_CountryId",
                schema: "BDataSchema",
                table: "UserHistories",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_EducationId",
                schema: "BDataSchema",
                table: "UserHistories",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_FinancialModeId",
                schema: "BDataSchema",
                table: "UserHistories",
                column: "FinancialModeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_GenderId",
                schema: "BDataSchema",
                table: "UserHistories",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_JobId",
                schema: "BDataSchema",
                table: "UserHistories",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_LanguageId",
                schema: "BDataSchema",
                table: "UserHistories",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_MaritalStatusId",
                schema: "BDataSchema",
                table: "UserHistories",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_PurposeId",
                schema: "BDataSchema",
                table: "UserHistories",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_UserPackageId",
                schema: "BDataSchema",
                table: "UserHistories",
                column: "UserPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_AppUserId",
                schema: "BDataSchema",
                table: "UserImages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserLikes",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserLikes",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "ASecurity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessageGroups_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserMessageGroups",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessageGroups_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserMessageGroups",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserMessages",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserMessages",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPackages_AppUserId",
                schema: "BDataSchema",
                table: "UserPackages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPackages_PackageId",
                schema: "BDataSchema",
                table: "UserPackages",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_AppUserId",
                schema: "BDataSchema",
                table: "UserPayments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserReports",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserReports",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "ASecurity",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "ASecurity",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "ASecurity",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokenTransactions_AppUserId",
                schema: "BDataSchema",
                table: "UserTokenTransactions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserViews_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserViews",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserViews_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserViews",
                column: "SenderAppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivateUsers",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "ContactUss",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "ForgotPasswordUsers",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "SendingEmails",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserBlocks",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserFavorites",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserHistories",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserImages",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserLikes",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserMessageGroups",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserMessages",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserPayments",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserReports",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserTokenTransactions",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserViews",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "Educations",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "FinancialModes",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "Genders",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "Jobs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "MaritalStatuses",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "Purposes",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserPackages",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "Packages",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "ASecurity");
        }
    }
}
