using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Heznek.DataAccess.Migrations
{
    public partial class AddDownloadName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AprovalDownloadName",
                table: "AcademicStudies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradesDownloadName",
                table: "AcademicStudies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AprovalDownloadName",
                table: "AcademicStudies");

            migrationBuilder.DropColumn(
                name: "GradesDownloadName",
                table: "AcademicStudies");
        }
    }
}
