using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class ComissionBadge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ComissionBadgeId",
                table: "Movements",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Movements_ComissionBadgeId",
                table: "Movements",
                column: "ComissionBadgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_BankAccounts_ComissionBadgeId",
                table: "Movements",
                column: "ComissionBadgeId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_BankAccounts_ComissionBadgeId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_ComissionBadgeId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "ComissionBadgeId",
                table: "Movements");
        }
    }
}
