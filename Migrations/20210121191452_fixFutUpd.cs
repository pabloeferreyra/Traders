using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class fixFutUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Gain",
                table: "FuturesUpdates",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Gain",
                table: "FuturesUpdates",
                type: "decimal(4,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");
        }
    }
}
