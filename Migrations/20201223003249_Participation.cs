using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class Participation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Participation",
                table: "Futures");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOperation",
                table: "Movements",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ParticipationId",
                table: "Futures",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Participations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Percentage = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Futures_ParticipationId",
                table: "Futures",
                column: "ParticipationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Futures_Participations_ParticipationId",
                table: "Futures",
                column: "ParticipationId",
                principalTable: "Participations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Futures_Participations_ParticipationId",
                table: "Futures");

            migrationBuilder.DropTable(
                name: "Participations");

            migrationBuilder.DropIndex(
                name: "IX_Futures_ParticipationId",
                table: "Futures");

            migrationBuilder.DropColumn(
                name: "DateOperation",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "ParticipationId",
                table: "Futures");

            migrationBuilder.AddColumn<int>(
                name: "Participation",
                table: "Futures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
