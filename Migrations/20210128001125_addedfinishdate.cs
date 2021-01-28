using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class addedfinishdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Futures_Clients_ClientId",
                table: "Futures");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "Futures",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "Futures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Futures_Clients_ClientId",
                table: "Futures",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Futures_Clients_ClientId",
                table: "Futures");

            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "Futures");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "Futures",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Futures_Clients_ClientId",
                table: "Futures",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
