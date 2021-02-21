using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class Retire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Retire",
                table: "Futures",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Retire",
                table: "Futures");
        }
    }
}
