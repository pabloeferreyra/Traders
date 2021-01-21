using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class ClientDiversities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Futures_Clients_ClientId",
                table: "Futures");

            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Badges_BadgesId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_BadgesId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "BadgeGuidIn",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "BadgeGuidOut",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "BadgesId",
                table: "Movements");

            migrationBuilder.AddColumn<string>(
                name: "BadgeIn",
                table: "Movements",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BadgeOut",
                table: "Movements",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "BadgesViewModelId",
                table: "Movements",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "Futures",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "clientDiversities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientCode = table.Column<int>(nullable: false),
                    AmmountBTC = table.Column<decimal>(type: "decimal(10,8)", nullable: false),
                    AmmountETH = table.Column<decimal>(type: "decimal(10,8)", nullable: false),
                    AmmountUSDT = table.Column<decimal>(type: "decimal(10,8)", nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientDiversities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movements_BadgesViewModelId",
                table: "Movements",
                column: "BadgesViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Futures_Clients_ClientId",
                table: "Futures",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Badges_BadgesViewModelId",
                table: "Movements",
                column: "BadgesViewModelId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Futures_Clients_ClientId",
                table: "Futures");

            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Badges_BadgesViewModelId",
                table: "Movements");

            migrationBuilder.DropTable(
                name: "clientDiversities");

            migrationBuilder.DropIndex(
                name: "IX_Movements_BadgesViewModelId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "BadgeIn",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "BadgeOut",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "BadgesViewModelId",
                table: "Movements");

            migrationBuilder.AddColumn<Guid>(
                name: "BadgeGuidIn",
                table: "Movements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BadgeGuidOut",
                table: "Movements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BadgesId",
                table: "Movements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "Futures",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movements_BadgesId",
                table: "Movements",
                column: "BadgesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Futures_Clients_ClientId",
                table: "Futures",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Badges_BadgesId",
                table: "Movements",
                column: "BadgesId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
