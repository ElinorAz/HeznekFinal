using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Heznek.DataAccess.Migrations
{
    public partial class AddVolunteerHoursAndChangesToCandidateProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ease",
                table: "Generals");

            migrationBuilder.AddColumn<string>(
                name: "Ease",
                table: "MilitaryServices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EaseOfService",
                table: "MilitaryServices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GeneralsParticipations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasFamilyDifficulties",
                table: "CandidateAdditionalData",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFinancialProblems",
                table: "CandidateAdditionalData",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHealthProblems",
                table: "CandidateAdditionalData",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BanksInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: false),
                    BankName = table.Column<string>(nullable: false),
                    BranchNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanksInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BanksInfo_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScholarDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CurrentYear = table.Column<int>(nullable: false),
                    Returns = table.Column<bool>(nullable: false),
                    ScholarName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScholarDetails_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Contribution = table.Column<string>(nullable: true),
                    Hours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerDetails_Profiles_Id",
                        column: x => x.Id,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerHours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityType = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    ProfileId = table.Column<int>(nullable: false),
                    Semester = table.Column<int>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerHours_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerHours_ProfileId",
                table: "VolunteerHours",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanksInfo");

            migrationBuilder.DropTable(
                name: "ScholarDetails");

            migrationBuilder.DropTable(
                name: "VolunteerDetails");

            migrationBuilder.DropTable(
                name: "VolunteerHours");

            migrationBuilder.DropColumn(
                name: "Ease",
                table: "MilitaryServices");

            migrationBuilder.DropColumn(
                name: "EaseOfService",
                table: "MilitaryServices");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GeneralsParticipations");

            migrationBuilder.DropColumn(
                name: "HasFamilyDifficulties",
                table: "CandidateAdditionalData");

            migrationBuilder.DropColumn(
                name: "HasFinancialProblems",
                table: "CandidateAdditionalData");

            migrationBuilder.DropColumn(
                name: "HasHealthProblems",
                table: "CandidateAdditionalData");

            migrationBuilder.AddColumn<string>(
                name: "Ease",
                table: "Generals",
                nullable: true);
        }
    }
}
