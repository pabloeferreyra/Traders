using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class finals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Futures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Participation = table.Column<int>(nullable: false),
                    Capital = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Futures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Futures_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuturesUpdates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<Guid>(nullable: false),
                    ModifDate = table.Column<DateTime>(nullable: false),
                    Gain = table.Column<decimal>(type: "decimal(4,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuturesUpdates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuturesUpdates_Futures_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Futures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Futures_ClientId",
                table: "Futures",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FuturesUpdates_ContractId",
                table: "FuturesUpdates",
                column: "ContractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuturesUpdates");

            migrationBuilder.DropTable(
                name: "Futures");
        }
    }
}
