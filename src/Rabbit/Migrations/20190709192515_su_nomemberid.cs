using Microsoft.EntityFrameworkCore.Migrations;

namespace Puma.Prey.Rabbit.Migrations
{
    public partial class su_nomemberid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "SafariUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "SafariUser",
                nullable: false,
                defaultValue: 0);
        }
    }
}
