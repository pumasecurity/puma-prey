using Microsoft.EntityFrameworkCore.Migrations;

namespace Puma.Prey.Rabbit.Migrations
{
    public partial class SafariUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Safaris_SafariId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_SafariUser_Safaris_SafariId",
                table: "SafariUser");

            migrationBuilder.DropIndex(
                name: "IX_SafariUser_SafariId",
                table: "SafariUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Safaris",
                table: "Safaris");

            migrationBuilder.RenameTable(
                name: "Safaris",
                newName: "Safari");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Safari",
                table: "Safari",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SafariUser_SafariId_PumaUserId",
                table: "SafariUser",
                columns: new[] { "SafariId", "PumaUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Safari_SafariId",
                table: "Animals",
                column: "SafariId",
                principalTable: "Safari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SafariUser_Safari_SafariId",
                table: "SafariUser",
                column: "SafariId",
                principalTable: "Safari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Safari_SafariId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_SafariUser_Safari_SafariId",
                table: "SafariUser");

            migrationBuilder.DropIndex(
                name: "IX_SafariUser_SafariId_PumaUserId",
                table: "SafariUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Safari",
                table: "Safari");

            migrationBuilder.RenameTable(
                name: "Safari",
                newName: "Safaris");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Safaris",
                table: "Safaris",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SafariUser_SafariId",
                table: "SafariUser",
                column: "SafariId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Safaris_SafariId",
                table: "Animals",
                column: "SafariId",
                principalTable: "Safaris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SafariUser_Safaris_SafariId",
                table: "SafariUser",
                column: "SafariId",
                principalTable: "Safaris",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
