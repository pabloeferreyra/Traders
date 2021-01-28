using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class FixRent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FixRent",
                table: "Futures",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixRent",
                table: "Futures");
        }
    }
}
