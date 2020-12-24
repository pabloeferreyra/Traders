using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Traders.Migrations
{
    public partial class correlationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CorrelationId",
                table: "Movements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrelationId",
                table: "Movements");
        }
    }
}
