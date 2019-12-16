using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.DataAccess.Migrations
{
    public partial class UserAplicationCompany_OperationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowEdit",
                table: "UserAplicationCompany",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowEdit",
                table: "UserAplicationCompany");
        }
    }
}
