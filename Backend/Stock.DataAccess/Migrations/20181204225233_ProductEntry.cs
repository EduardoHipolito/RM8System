using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stock.DataAccess.Migrations
{
    public partial class ProductEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(maxLength: 2, nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    IdSupplier = table.Column<int>(nullable: true),
                    TotalPrice = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Shipping = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entry_Supplier_IdSupplier",
                        column: x => x.IdSupplier,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductEntry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(maxLength: 2, nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    IdProduct = table.Column<int>(nullable: false),
                    IdEntry = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    ICMS = table.Column<double>(nullable: false),
                    IPI = table.Column<double>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEntry_Entry_IdEntry",
                        column: x => x.IdEntry,
                        principalTable: "Entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductEntry_Product_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entry_IdSupplier",
                table: "Entry",
                column: "IdSupplier");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntry_IdEntry",
                table: "ProductEntry",
                column: "IdEntry");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntry_IdProduct",
                table: "ProductEntry",
                column: "IdProduct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEntry");

            migrationBuilder.DropTable(
                name: "Entry");
        }
    }
}
