using Microsoft.EntityFrameworkCore.Migrations;

namespace Puma.Prey.Rabbit.Migrations
{
    public partial class Safariupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Safari_SafariId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_SafariUser_PumaUsers_PumaUserId",
                table: "SafariUser");

            migrationBuilder.DropForeignKey(
                name: "FK_SafariUser_Safari_SafariId",
                table: "SafariUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SafariUser",
                table: "SafariUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Safari",
                table: "Safari");

            migrationBuilder.RenameTable(
                name: "SafariUser",
                newName: "SafariUsers");

            migrationBuilder.RenameTable(
                name: "Safari",
                newName: "Safaris");

            migrationBuilder.RenameIndex(
                name: "IX_SafariUser_SafariId_PumaUserId",
                table: "SafariUsers",
                newName: "IX_SafariUsers_SafariId_PumaUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SafariUser_PumaUserId",
                table: "SafariUsers",
                newName: "IX_SafariUsers_PumaUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SafariUsers",
                table: "SafariUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Safaris",
                table: "Safaris",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Safaris_SafariId",
                table: "Animals",
                column: "SafariId",
                principalTable: "Safaris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SafariUsers_PumaUsers_PumaUserId",
                table: "SafariUsers",
                column: "PumaUserId",
                principalTable: "PumaUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SafariUsers_Safaris_SafariId",
                table: "SafariUsers",
                column: "SafariId",
                principalTable: "Safaris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Safaris_SafariId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_SafariUsers_PumaUsers_PumaUserId",
                table: "SafariUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SafariUsers_Safaris_SafariId",
                table: "SafariUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SafariUsers",
                table: "SafariUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Safaris",
                table: "Safaris");

            migrationBuilder.RenameTable(
                name: "SafariUsers",
                newName: "SafariUser");

            migrationBuilder.RenameTable(
                name: "Safaris",
                newName: "Safari");

            migrationBuilder.RenameIndex(
                name: "IX_SafariUsers_SafariId_PumaUserId",
                table: "SafariUser",
                newName: "IX_SafariUser_SafariId_PumaUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SafariUsers_PumaUserId",
                table: "SafariUser",
                newName: "IX_SafariUser_PumaUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SafariUser",
                table: "SafariUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Safari",
                table: "Safari",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Safari_SafariId",
                table: "Animals",
                column: "SafariId",
                principalTable: "Safari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SafariUser_PumaUsers_PumaUserId",
                table: "SafariUser",
                column: "PumaUserId",
                principalTable: "PumaUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SafariUser_Safari_SafariId",
                table: "SafariUser",
                column: "SafariId",
                principalTable: "Safari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
