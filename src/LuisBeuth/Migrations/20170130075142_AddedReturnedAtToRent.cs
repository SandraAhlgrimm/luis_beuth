using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace luis_beuth.Migrations
{
    public partial class AddedReturnedAtToRent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnedAt",
                table: "Rent",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnedAt",
                table: "Rent");
        }
    }
}
