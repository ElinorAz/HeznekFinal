using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Heznek.DataAccess.Migrations
{
    public partial class StudentScholarship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentYear",
                table: "ScholarDetails");

            migrationBuilder.DropColumn(
                name: "ScholarName",
                table: "ScholarDetails");

            migrationBuilder.RenameColumn(
                name: "Returns",
                table: "ScholarDetails",
                newName: "Refund");

            migrationBuilder.AddColumn<decimal>(
                name: "April",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "August",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "December",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "February",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "January",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "July",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "June",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "March",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "May",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "November",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "October",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "September",
                table: "StudentsScholarships",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Budgeting",
                table: "ScholarDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "April",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "August",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "December",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "February",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "January",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "July",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "June",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "March",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "May",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "November",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "October",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "September",
                table: "StudentsScholarships");

            migrationBuilder.DropColumn(
                name: "Budgeting",
                table: "ScholarDetails");

            migrationBuilder.RenameColumn(
                name: "Refund",
                table: "ScholarDetails",
                newName: "Returns");

            migrationBuilder.AddColumn<int>(
                name: "CurrentYear",
                table: "ScholarDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ScholarName",
                table: "ScholarDetails",
                nullable: false,
                defaultValue: "");
        }
    }
}
