using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.DataAccess.Migrations
{
    public partial class AplicationAndModuleCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModuleCode",
                table: "Module",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AplicationCode",
                table: "Aplication",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModuleCode",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "AplicationCode",
                table: "Aplication");
        }
    }
}
