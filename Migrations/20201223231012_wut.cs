using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class wut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Badges_BadgesId",
                table: "Movements");

            migrationBuilder.DropForeignKey(
                name: "FK_Movements_BankAccounts_BankAccountsId",
                table: "Movements");

            migrationBuilder.AlterColumn<Guid>(
                name: "BankAccountsId",
                table: "Movements",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BadgesId",
                table: "Movements",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Badges_BadgesId",
                table: "Movements",
                column: "BadgesId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_BankAccounts_BankAccountsId",
                table: "Movements",
                column: "BankAccountsId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Badges_BadgesId",
                table: "Movements");

            migrationBuilder.DropForeignKey(
                name: "FK_Movements_BankAccounts_BankAccountsId",
                table: "Movements");

            migrationBuilder.AlterColumn<Guid>(
                name: "BankAccountsId",
                table: "Movements",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BadgesId",
                table: "Movements",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Badges_BadgesId",
                table: "Movements",
                column: "BadgesId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_BankAccounts_BankAccountsId",
                table: "Movements",
                column: "BankAccountsId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
