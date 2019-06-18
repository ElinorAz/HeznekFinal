using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Heznek.DataAccess.Migrations
{
    public partial class AddCandidateForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormsParentsSalary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Disability = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    FormId = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: true),
                    MotherName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Parent = table.Column<int>(nullable: true),
                    SalarySlips = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormsParentsSalary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormsParentsSalary_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    FormId = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormTasks_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forms_UserId",
                table: "Forms",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormsParentsSalary_FormId",
                table: "FormsParentsSalary",
                column: "FormId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormTasks_FormId",
                table: "FormTasks",
                column: "FormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormsParentsSalary");

            migrationBuilder.DropTable(
                name: "FormTasks");

            migrationBuilder.DropTable(
                name: "Forms");
        }
    }
}
