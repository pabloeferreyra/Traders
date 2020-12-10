using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class movementsalpha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AmountIn",
                table: "Movements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmountOut",
                table: "Movements",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BadgeGuidIn",
                table: "Movements",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BadgeGuidOut",
                table: "Movements",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BadgesId",
                table: "Movements",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Badges",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movements_BadgesId",
                table: "Movements",
                column: "BadgesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Badges_BadgesId",
                table: "Movements",
                column: "BadgesId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Badges_BadgesId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_BadgesId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "AmountIn",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "AmountOut",
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Badges",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
