using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Heznek.DataAccess.Migrations
{
    public partial class AddGivenInPastAndFinishTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GivenInPast",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FinishTime",
                table: "Events",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GivenInPast",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "Events");
        }
    }
}
