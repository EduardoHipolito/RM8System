using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.DataAccess.Migrations
{
    public partial class Core2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UniquePermition",
                table: "UserAplicationCompany");

            migrationBuilder.AlterColumn<int>(
                name: "IdUser",
                table: "UserAplicationCompany",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "IdCompany",
                table: "UserAplicationCompany",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UniquePermition",
                table: "UserAplicationCompany",
                columns: new[] { "IdAplication", "IdCompany", "IdUser" },
                unique: true,
                filter: "[IdCompany] IS NOT NULL AND [IdUser] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UniquePermition",
                table: "UserAplicationCompany");

            migrationBuilder.AlterColumn<int>(
                name: "IdUser",
                table: "UserAplicationCompany",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdCompany",
                table: "UserAplicationCompany",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UniquePermition",
                table: "UserAplicationCompany",
                columns: new[] { "IdAplication", "IdCompany", "IdUser" },
                unique: true);
        }
    }
}
