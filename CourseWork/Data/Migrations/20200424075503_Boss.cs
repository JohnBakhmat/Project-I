using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseWork.Data.Migrations
{
    public partial class Boss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisation_Criminal_BossId",
                table: "Organisation");

            migrationBuilder.DropIndex(
                name: "IX_Organisation_BossId",
                table: "Organisation");

            migrationBuilder.DropColumn(
                name: "BossId",
                table: "Organisation");

            migrationBuilder.AddColumn<int>(
                name: "Boss",
                table: "Organisation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Boss",
                table: "Organisation");

            migrationBuilder.AddColumn<int>(
                name: "BossId",
                table: "Organisation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organisation_BossId",
                table: "Organisation",
                column: "BossId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisation_Criminal_BossId",
                table: "Organisation",
                column: "BossId",
                principalTable: "Criminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
