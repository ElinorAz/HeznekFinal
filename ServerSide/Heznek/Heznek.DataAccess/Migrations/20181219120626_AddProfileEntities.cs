using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Heznek.DataAccess.Migrations
{
    public partial class AddProfileEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Confirmations_Users_UserId",
                table: "Confirmations");

            migrationBuilder.DropIndex(
                name: "IX_Confirmations_UserId",
                table: "Confirmations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Confirmations");

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcademicParents = table.Column<int>(nullable: false, defaultValue: 0),
                    Address = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Siblings = table.Column<int>(nullable: false, defaultValue: 0),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicStudies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    AcademicDegree = table.Column<string>(nullable: true),
                    AcademicInstitution = table.Column<string>(nullable: true),
                    AprovalFileName = table.Column<string>(nullable: true),
                    FieldOfStudy = table.Column<string>(nullable: true),
                    GradesFileName = table.Column<string>(nullable: true),
                    GraduationYear = table.Column<int>(nullable: true),
                    Residence = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicStudies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicStudies_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CandidateAdditionalData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Difficulties = table.Column<string>(nullable: true),
                    FamilyDifficulties = table.Column<string>(nullable: true),
                    FinancialProblems = table.Column<string>(nullable: true),
                    HealthProblems = table.Column<string>(nullable: true),
                    LifeStory = table.Column<string>(nullable: true),
                    ParticipationDescription = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    SituationDetails = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateAdditionalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateAdditionalData_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Generals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Disabilities = table.Column<bool>(nullable: false),
                    Ease = table.Column<string>(nullable: true),
                    ParticipationInPrograms = table.Column<int>(nullable: false, defaultValue: 0),
                    Points = table.Column<int>(nullable: false, defaultValue: 0),
                    PsychometricGrade = table.Column<int>(nullable: false, defaultValue: 0),
                    WorthyOfAdvancment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Generals_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HighSchools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighSchools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HighSchools_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    TypeOfSevice = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MilitaryServices_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Confirmations_Users_Id",
                table: "Confirmations",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Confirmations_Users_Id",
                table: "Confirmations");

            migrationBuilder.DropTable(
                name: "AcademicStudies");

            migrationBuilder.DropTable(
                name: "CandidateAdditionalData");

            migrationBuilder.DropTable(
                name: "Generals");

            migrationBuilder.DropTable(
                name: "HighSchools");

            migrationBuilder.DropTable(
                name: "MilitaryServices");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Confirmations",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Confirmations_UserId",
                table: "Confirmations",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Confirmations_Users_UserId",
                table: "Confirmations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
