using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Puma.Prey.Rabbit.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PumaRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PumaRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PumaUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    CreditCardNumber = table.Column<string>(nullable: true),
                    CreditCardExpiration = table.Column<string>(nullable: true),
                    BillingAddress1 = table.Column<string>(nullable: true),
                    BillingAddress2 = table.Column<string>(nullable: true),
                    BillingCity = table.Column<string>(nullable: true),
                    BillingState = table.Column<string>(nullable: true),
                    BillingZip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PumaUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Safaris",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Safaris", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PumaRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PumaRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PumaRoleClaims_PumaRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "PumaRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PumaUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PumaUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PumaUserClaims_PumaUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "PumaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PumaUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PumaUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_PumaUserLogins_PumaUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "PumaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PumaUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PumaUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PumaUserRoles_PumaRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "PumaRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PumaUserRoles_PumaUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "PumaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PumaUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PumaUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_PumaUserTokens_PumaUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "PumaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SafariId = table.Column<int>(nullable: false),
                    AnimalName = table.Column<string>(nullable: true),
                    Species = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_Safaris_SafariId",
                        column: x => x.SafariId,
                        principalTable: "Safaris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafariUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SafariId = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    PumaUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafariUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SafariUser_PumaUsers_PumaUserId",
                        column: x => x.PumaUserId,
                        principalTable: "PumaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SafariUser_Safaris_SafariId",
                        column: x => x.SafariId,
                        principalTable: "Safaris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_SafariId",
                table: "Animals",
                column: "SafariId");

            migrationBuilder.CreateIndex(
                name: "IX_PumaRoleClaims_RoleId",
                table: "PumaRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "PumaRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PumaUserClaims_UserId",
                table: "PumaUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PumaUserLogins_UserId",
                table: "PumaUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PumaUserRoles_RoleId",
                table: "PumaUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PumaUsers_MemberId",
                table: "PumaUsers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "PumaUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "PumaUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SafariUser_PumaUserId",
                table: "SafariUser",
                column: "PumaUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SafariUser_SafariId",
                table: "SafariUser",
                column: "SafariId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "PumaRoleClaims");

            migrationBuilder.DropTable(
                name: "PumaUserClaims");

            migrationBuilder.DropTable(
                name: "PumaUserLogins");

            migrationBuilder.DropTable(
                name: "PumaUserRoles");

            migrationBuilder.DropTable(
                name: "PumaUserTokens");

            migrationBuilder.DropTable(
                name: "SafariUser");

            migrationBuilder.DropTable(
                name: "PumaRoles");

            migrationBuilder.DropTable(
                name: "PumaUsers");

            migrationBuilder.DropTable(
                name: "Safaris");
        }
    }
}
