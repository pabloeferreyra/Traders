using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class updatemovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountGuidIn",
                table: "Movements",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountGuidOut",
                table: "Movements",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountsId",
                table: "Movements",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Movements_BankAccountsId",
                table: "Movements",
                column: "BankAccountsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_BankAccounts_BankAccountsId",
                table: "Movements",
                column: "BankAccountsId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_BankAccounts_BankAccountsId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_BankAccountsId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "BankAccountGuidIn",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "BankAccountGuidOut",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "BankAccountsId",
                table: "Movements");
        }
    }
}
