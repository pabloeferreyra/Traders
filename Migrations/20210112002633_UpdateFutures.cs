using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class UpdateFutures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractNumber",
                table: "Futures",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractNumber",
                table: "Futures");
        }
    }
}
