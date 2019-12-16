using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.DataAccess.Migrations
{
    public partial class ShowApplicationInMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowMenu",
                table: "Aplication",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowMenu",
                table: "Aplication");
        }
    }
}
