using Microsoft.EntityFrameworkCore.Migrations;

namespace Stock.DataAccess.Migrations
{
    public partial class Entry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Supplier_IdSupplier",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_IdSupplier",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IdSupplier",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Product",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Product",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Product",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdSupplier",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdSupplier",
                table: "Product",
                column: "IdSupplier");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Supplier_IdSupplier",
                table: "Product",
                column: "IdSupplier",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
