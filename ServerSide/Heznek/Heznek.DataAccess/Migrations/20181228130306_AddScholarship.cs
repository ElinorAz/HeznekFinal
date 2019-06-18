using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Heznek.DataAccess.Migrations
{
    public partial class AddScholarship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DownloadName",
                table: "FormTasks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisabilityDownloadName",
                table: "FormsParentsSalary",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalarySlipsDownloadName",
                table: "FormsParentsSalary",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Scholarship",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Admission = table.Column<string>(nullable: false),
                    Budget = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scholarship", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentsScholarships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfileId = table.Column<int>(nullable: false),
                    ScholarshipId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsScholarships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsScholarships_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentsScholarships_Scholarship_ScholarshipId",
                        column: x => x.ScholarshipId,
                        principalTable: "Scholarship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsScholarships_ProfileId",
                table: "StudentsScholarships",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsScholarships_ScholarshipId",
                table: "StudentsScholarships",
                column: "ScholarshipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsScholarships");

            migrationBuilder.DropTable(
                name: "Scholarship");

            migrationBuilder.DropColumn(
                name: "DownloadName",
                table: "FormTasks");

            migrationBuilder.DropColumn(
                name: "DisabilityDownloadName",
                table: "FormsParentsSalary");

            migrationBuilder.DropColumn(
                name: "SalarySlipsDownloadName",
                table: "FormsParentsSalary");
        }
    }
}
