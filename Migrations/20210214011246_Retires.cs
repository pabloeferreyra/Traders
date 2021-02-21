using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class Retires : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Retires",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractNumber = table.Column<int>(nullable: false),
                    RetireDate = table.Column<DateTime>(nullable: false),
                    Capital = table.Column<decimal>(type: "decimal(30,10)", nullable: false),
                    RetireCapital = table.Column<decimal>(type: "decimal(30,10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retires", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Retires");
        }
    }
}
