using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Data.Migrations
{
    public partial class heightAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Criminal",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BirthPlace",
                table: "Criminal",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Criminal",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Criminal");

            migrationBuilder.DropColumn(
                name: "BirthPlace",
                table: "Criminal");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Criminal");
        }
    }
}
