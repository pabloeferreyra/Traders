using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class AddedFinalResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinalResult",
                table: "Futures",
                type: "decimal(30,10)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalResult",
                table: "Futures");
        }
    }
}
