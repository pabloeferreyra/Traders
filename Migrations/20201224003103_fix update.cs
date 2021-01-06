using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class fixupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuturesUpdates_Futures_ContractId",
                table: "FuturesUpdates");

            migrationBuilder.DropIndex(
                name: "IX_FuturesUpdates_ContractId",
                table: "FuturesUpdates");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "FuturesUpdates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContractId",
                table: "FuturesUpdates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FuturesUpdates_ContractId",
                table: "FuturesUpdates",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuturesUpdates_Futures_ContractId",
                table: "FuturesUpdates",
                column: "ContractId",
                principalTable: "Futures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
