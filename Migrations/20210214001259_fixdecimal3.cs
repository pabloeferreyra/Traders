using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class fixdecimal3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_clientDiversities",
                table: "clientDiversities");

            migrationBuilder.RenameTable(
                name: "clientDiversities",
                newName: "ClientDiversities");

            migrationBuilder.AlterColumn<double>(
                name: "Comission",
                table: "Movements",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "AmountOut",
                table: "Movements",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "AmountIn",
                table: "Movements",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

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
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "Capital",
                table: "Futures",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "BankAccounts",
                type: "float(53)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientDiversities",
                table: "ClientDiversities",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientDiversities",
                table: "ClientDiversities");

            migrationBuilder.RenameTable(
                name: "ClientDiversities",
                newName: "clientDiversities");

            migrationBuilder.AlterColumn<double>(
                name: "Comission",
                table: "Movements",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");

            migrationBuilder.AlterColumn<double>(
                name: "AmountOut",
                table: "Movements",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");

            migrationBuilder.AlterColumn<double>(
                name: "AmountIn",
                table: "Movements",
                type: "double",
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

            migrationBuilder.AlterColumn<double>(
                name: "FixRentPercentage",
                table: "Futures",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");

            migrationBuilder.AlterColumn<double>(
                name: "Capital",
                table: "Futures",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "BankAccounts",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(53)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clientDiversities",
                table: "clientDiversities",
                column: "Id");
        }
    }
}
