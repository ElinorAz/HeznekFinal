using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Heznek.DataAccess.Migrations
{
    public partial class CandidateFormDisability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parent",
                table: "FormsParentsSalary");

            migrationBuilder.AddColumn<string>(
                name: "Disability2",
                table: "FormsParentsSalary",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Disability2DownloadName",
                table: "FormsParentsSalary",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FatherDisability",
                table: "FormsParentsSalary",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MotherDisability",
                table: "FormsParentsSalary",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disability2",
                table: "FormsParentsSalary");

            migrationBuilder.DropColumn(
                name: "Disability2DownloadName",
                table: "FormsParentsSalary");

            migrationBuilder.DropColumn(
                name: "FatherDisability",
                table: "FormsParentsSalary");

            migrationBuilder.DropColumn(
                name: "MotherDisability",
                table: "FormsParentsSalary");

            migrationBuilder.AddColumn<int>(
                name: "Parent",
                table: "FormsParentsSalary",
                nullable: true);
        }
    }
}
