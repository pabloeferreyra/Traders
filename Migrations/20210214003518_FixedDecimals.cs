using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class FixedDecimals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Comission",
                table: "Movements",
                type: "decimal(30,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountOut",
                table: "Movements",
                type: "decimal(30,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountIn",
                table: "Movements",
                type: "decimal(30,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gain",
                table: "FuturesUpdates",
                type: "decimal(30,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FixRentPercentage",
                table: "Futures",
                type: "decimal(30,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Capital",
                table: "Futures",
                type: "decimal(30,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "BankAccounts",
                type: "decimal(30,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Comission",
                table: "Movements",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountOut",
                table: "Movements",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountIn",
                table: "Movements",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gain",
                table: "FuturesUpdates",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FixRentPercentage",
                table: "Futures",
                type: "decimal(2,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Capital",
                table: "Futures",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "BankAccounts",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,10)");
        }
    }
}
