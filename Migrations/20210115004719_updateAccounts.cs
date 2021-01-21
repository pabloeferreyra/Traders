using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class updateAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "BankAccounts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "BankAccounts");
        }
    }
}
