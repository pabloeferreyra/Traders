using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class fixdecimal4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Comission",
                table: "Movements",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountOut",
                table: "Movements",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountIn",
                table: "Movements",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gain",
                table: "FuturesUpdates",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FixRentPercentage",
                table: "Futures",
                type: "decimal(2,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Capital",
                table: "Futures",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "BankAccounts",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Comission",
                table: "Movements",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.AlterColumn<double>(
                name: "AmountOut",
                table: "Movements",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.AlterColumn<double>(
                name: "AmountIn",
                table: "Movements",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.AlterColumn<double>(
                name: "Gain",
                table: "FuturesUpdates",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.AlterColumn<double>(
                name: "FixRentPercentage",
                table: "Futures",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Capital",
                table: "Futures",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "BankAccounts",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");
        }
    }
}
