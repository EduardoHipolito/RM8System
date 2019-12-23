using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cnae",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cnae", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Code = table.Column<string>(maxLength: 5, nullable: false),
                    PhoneCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: false),
                    ModuleCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Email = table.Column<string>(maxLength: 130, nullable: false),
                    HomePage = table.Column<string>(maxLength: 80, nullable: true),
                    Type = table.Column<int>(nullable: false),
                    FantasyName = table.Column<string>(maxLength: 100, nullable: true),
                    CorporateName = table.Column<string>(maxLength: 100, nullable: true),
                    IdCnae = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Cnae_IdCnae",
                        column: x => x.IdCnae,
                        principalTable: "Cnae",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    PersonType = table.Column<byte>(maxLength: 30, nullable: false),
                    IdCountry = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentType_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Code = table.Column<string>(maxLength: 5, nullable: false),
                    IdCountry = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aplication",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    Link = table.Column<string>(maxLength: 100, nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IdModule = table.Column<int>(nullable: false),
                    AplicationCode = table.Column<string>(nullable: false),
                    ShowMenu = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aplication_Module_IdModule",
                        column: x => x.IdModule,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    PaymentDay = table.Column<int>(nullable: false),
                    IdPerson = table.Column<int>(nullable: false),
                    IdMaster = table.Column<int>(nullable: true),
                    ReducedName = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Company_IdMaster",
                        column: x => x.IdMaster,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_Person_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    Type = table.Column<int>(maxLength: 2, nullable: false),
                    IdCountry = table.Column<int>(nullable: false),
                    AreaCode = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    IdPerson = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phone_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Phone_Person_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    Login = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(maxLength: 64, nullable: true),
                    ProfileType = table.Column<int>(maxLength: 2, nullable: false),
                    TokenAlteracaoDeSenha = table.Column<Guid>(nullable: false),
                    IdPerson = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Person_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    IdDocumentType = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 30, nullable: false),
                    IdPerson = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_DocumentType_IdDocumentType",
                        column: x => x.IdDocumentType,
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Document_Person_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    IdCountry = table.Column<int>(nullable: false),
                    IdState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_State_IdState",
                        column: x => x.IdState,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAplicationCompany",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    IdAplication = table.Column<int>(nullable: false),
                    IdUser = table.Column<int>(nullable: true),
                    IdCompany = table.Column<int>(nullable: true),
                    AccessLevel = table.Column<int>(nullable: false),
                    IsGlobal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAplicationCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAplicationCompany_Aplication_IdAplication",
                        column: x => x.IdAplication,
                        principalTable: "Aplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAplicationCompany_Company_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAplicationCompany_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IdCompanyPermition = table.Column<int>(nullable: true),
                    IdPerson = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    PublicAreaType = table.Column<int>(nullable: false),
                    PublicArea = table.Column<string>(maxLength: 130, nullable: false),
                    Complement = table.Column<string>(maxLength: 20, nullable: true),
                    Number = table.Column<int>(nullable: false),
                    Neighborhood = table.Column<string>(maxLength: 50, nullable: false),
                    IdCountry = table.Column<int>(nullable: false),
                    IdState = table.Column<int>(nullable: true),
                    IdCity = table.Column<int>(nullable: true),
                    PostalCode = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_City_IdCity",
                        column: x => x.IdCity,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_Person_IdPerson",
                        column: x => x.IdPerson,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_State_IdState",
                        column: x => x.IdState,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_IdCity",
                table: "Address",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_Address_IdCountry",
                table: "Address",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Address_IdPerson",
                table: "Address",
                column: "IdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_Address_IdState",
                table: "Address",
                column: "IdState");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueAplicationModuleIndex",
                table: "Aplication",
                columns: new[] { "IdModule", "Index" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UniqueAplication",
                table: "Aplication",
                columns: new[] { "Name", "Description" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_IdCountry",
                table: "City",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_City_IdState",
                table: "City",
                column: "IdState");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueCnae",
                table: "Cnae",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_IdMaster",
                table: "Company",
                column: "IdMaster");

            migrationBuilder.CreateIndex(
                name: "IX_Company_IdPerson",
                table: "Company",
                column: "IdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueCountry",
                table: "Country",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_IdDocumentType",
                table: "Document",
                column: "IdDocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_Document_IdPerson",
                table: "Document",
                column: "IdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueDocument",
                table: "Document",
                columns: new[] { "Value", "IdDocumentType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_IdCountry",
                table: "DocumentType",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueModule",
                table: "Module",
                columns: new[] { "Name", "Description" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_IdCnae",
                table: "Person",
                column: "IdCnae");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_IdCountry",
                table: "Phone",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_IdPerson",
                table: "Phone",
                column: "IdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_State_IdCountry",
                table: "State",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueEstado",
                table: "State",
                columns: new[] { "Name", "IdCountry" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_IdPerson",
                table: "User",
                column: "IdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_IniqueUser",
                table: "User",
                columns: new[] { "Login", "IdPerson" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAplicationCompany_IdCompany",
                table: "UserAplicationCompany",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_UserAplicationCompany_IdUser",
                table: "UserAplicationCompany",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UniquePermition",
                table: "UserAplicationCompany",
                columns: new[] { "IdAplication", "IdCompany", "IdUser" },
                unique: true,
                filter: "[IdCompany] IS NOT NULL AND [IdUser] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "UserAplicationCompany");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "Aplication");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Cnae");
        }
    }
}
