using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class FixParticipation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Futures_Participations_ParticipationId",
                table: "Futures");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParticipationId",
                table: "Futures",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Futures_Participations_ParticipationId",
                table: "Futures",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Futures_Participations_ParticipationId",
                table: "Futures");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParticipationId",
                table: "Futures",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Futures_Participations_ParticipationId",
                table: "Futures",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
