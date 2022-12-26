using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiniECommerce.Persistence.Migrations
{
    public partial class mig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandCode",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Showcase",
                table: "Files",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.UniqueConstraint("AK_Brands_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "BrandBrandLogoFile",
                columns: table => new
                {
                    BrandLogoFilesId = table.Column<Guid>(type: "uuid", nullable: false),
                    BrandsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandBrandLogoFile", x => new { x.BrandLogoFilesId, x.BrandsId });
                    table.ForeignKey(
                        name: "FK_BrandBrandLogoFile_Brands_BrandsId",
                        column: x => x.BrandsId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandBrandLogoFile_Files_BrandLogoFilesId",
                        column: x => x.BrandLogoFilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandCode",
                table: "Products",
                column: "BrandCode");

            migrationBuilder.CreateIndex(
                name: "IX_BrandBrandLogoFile_BrandsId",
                table: "BrandBrandLogoFile",
                column: "BrandsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandCode",
                table: "Products",
                column: "BrandCode",
                principalTable: "Brands",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandCode",
                table: "Products");

            migrationBuilder.DropTable(
                name: "BrandBrandLogoFile");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Showcase",
                table: "Files");
        }
    }
}
