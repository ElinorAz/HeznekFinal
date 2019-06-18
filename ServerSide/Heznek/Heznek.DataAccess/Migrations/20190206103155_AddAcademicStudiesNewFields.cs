using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Heznek.DataAccess.Migrations
{
    public partial class AddAcademicStudiesNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BeginningDegree",
                table: "AcademicStudies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundStudies",
                table: "AcademicStudies",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OtherFundings",
                table: "AcademicStudies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StudyYear",
                table: "AcademicStudies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tuition",
                table: "AcademicStudies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfDegree",
                table: "AcademicStudies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginningDegree",
                table: "AcademicStudies");

            migrationBuilder.DropColumn(
                name: "FundStudies",
                table: "AcademicStudies");

            migrationBuilder.DropColumn(
                name: "OtherFundings",
                table: "AcademicStudies");

            migrationBuilder.DropColumn(
                name: "StudyYear",
                table: "AcademicStudies");

            migrationBuilder.DropColumn(
                name: "Tuition",
                table: "AcademicStudies");

            migrationBuilder.DropColumn(
                name: "TypeOfDegree",
                table: "AcademicStudies");
        }
    }
}
