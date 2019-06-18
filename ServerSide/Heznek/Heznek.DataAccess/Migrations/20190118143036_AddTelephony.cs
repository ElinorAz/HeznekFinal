using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Heznek.DataAccess.Migrations
{
    public partial class AddTelephony : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Telephonies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    DateBackFirst = table.Column<DateTime>(nullable: true),
                    DateBackSecond = table.Column<DateTime>(nullable: true),
                    DateBackThird = table.Column<DateTime>(nullable: true),
                    FundingAvailability = table.Column<bool>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Thoughts = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephonies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telephonies_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telephonies");
        }
    }
}
