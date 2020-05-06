using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Data.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crime",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Punishment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Criminal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    Age = table.Column<byte>(nullable: false),
                    Sex = table.Column<string>(nullable: true),
                    HairColor = table.Column<string>(nullable: true),
                    EyeColor = table.Column<string>(nullable: true),
                    ExtraSigns = table.Column<string>(nullable: true),
                    LastAccomodation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criminal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrimeCriminal",
                columns: table => new
                {
                    CriminalId = table.Column<int>(nullable: false),
                    CrimeId = table.Column<int>(nullable: false),
                    ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrimeCriminal", x => new { x.CriminalId, x.CrimeId });
                    table.ForeignKey(
                        name: "FK_CrimeCriminal_Crime_CrimeId",
                        column: x => x.CrimeId,
                        principalTable: "Crime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeCriminal_Criminal_CriminalId",
                        column: x => x.CriminalId,
                        principalTable: "Criminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organisation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    BossId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organisation_Criminal_BossId",
                        column: x => x.BossId,
                        principalTable: "Criminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CriminalOrganisation",
                columns: table => new
                {
                    CriminalId = table.Column<int>(nullable: false),
                    OrganisationId = table.Column<int>(nullable: false),
                    ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriminalOrganisation", x => new { x.CriminalId, x.OrganisationId });
                    table.ForeignKey(
                        name: "FK_CriminalOrganisation_Criminal_CriminalId",
                        column: x => x.CriminalId,
                        principalTable: "Criminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriminalOrganisation_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrimeCriminal_CrimeId",
                table: "CrimeCriminal",
                column: "CrimeId");

            migrationBuilder.CreateIndex(
                name: "IX_CriminalOrganisation_OrganisationId",
                table: "CriminalOrganisation",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisation_BossId",
                table: "Organisation",
                column: "BossId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrimeCriminal");

            migrationBuilder.DropTable(
                name: "CriminalOrganisation");

            migrationBuilder.DropTable(
                name: "Crime");

            migrationBuilder.DropTable(
                name: "Organisation");

            migrationBuilder.DropTable(
                name: "Criminal");
        }
    }
}
