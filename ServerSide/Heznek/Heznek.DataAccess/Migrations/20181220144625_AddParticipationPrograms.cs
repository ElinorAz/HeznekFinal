using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Heznek.DataAccess.Migrations
{
    public partial class AddParticipationPrograms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipationInPrograms",
                table: "Generals");

            migrationBuilder.CreateTable(
                name: "ParticipationPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProgramName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipationPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralsParticipations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GeneralId = table.Column<int>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralsParticipations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralsParticipations_Generals_GeneralId",
                        column: x => x.GeneralId,
                        principalTable: "Generals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneralsParticipations_ParticipationPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "ParticipationPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralsParticipations_GeneralId",
                table: "GeneralsParticipations",
                column: "GeneralId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralsParticipations_ProgramId",
                table: "GeneralsParticipations",
                column: "ProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralsParticipations");

            migrationBuilder.DropTable(
                name: "ParticipationPrograms");

            migrationBuilder.AddColumn<int>(
                name: "ParticipationInPrograms",
                table: "Generals",
                nullable: false,
                defaultValue: 0);
        }
    }
}
