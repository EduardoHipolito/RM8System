using Microsoft.EntityFrameworkCore.Migrations;

namespace Stock.DataAccess.Migrations
{
    public partial class IsOwnProductionInProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOwnProduction",
                table: "Product",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOwnProduction",
                table: "Product");
        }
    }
}
