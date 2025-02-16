using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elysian_Web.Migrations
{
    /// <inheritdoc />
    public partial class CategoriesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billboard_Stores_StoreId",
                table: "Billboard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Billboard",
                table: "Billboard");

            migrationBuilder.RenameTable(
                name: "Billboard",
                newName: "Billboards");

            migrationBuilder.RenameIndex(
                name: "IX_Billboard_StoreId",
                table: "Billboards",
                newName: "IX_Billboards_StoreId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoriesId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Billboards",
                table: "Billboards",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    BillBoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Billboards_BillBoardId",
                        column: x => x.BillBoardId,
                        principalTable: "Billboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoriesId",
                table: "Products",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BillBoardId",
                table: "Categories",
                column: "BillBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_StoreId",
                table: "Categories",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Billboards_Stores_StoreId",
                table: "Billboards",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoriesId",
                table: "Products",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billboards_Stores_StoreId",
                table: "Billboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoriesId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoriesId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Billboards",
                table: "Billboards");

            migrationBuilder.DropColumn(
                name: "CategoriesId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Billboards",
                newName: "Billboard");

            migrationBuilder.RenameIndex(
                name: "IX_Billboards_StoreId",
                table: "Billboard",
                newName: "IX_Billboard_StoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Billboard",
                table: "Billboard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Billboard_Stores_StoreId",
                table: "Billboard",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
