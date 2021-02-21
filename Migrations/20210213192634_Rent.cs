using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class Rent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FixRentPercentage",
                table: "Futures",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixRentPercentage",
                table: "Futures");
        }
    }
}
