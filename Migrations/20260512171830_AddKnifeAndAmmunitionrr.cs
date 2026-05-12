using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GunShop.Migrations
{
    /// <inheritdoc />
    public partial class AddKnifeAndAmmunitionrr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmmunitionId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KnifeId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ammunitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Caliber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ammunitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ammunitions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Knives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BladeMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BladeLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Knives_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_AmmunitionId",
                table: "OrderItems",
                column: "AmmunitionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_KnifeId",
                table: "OrderItems",
                column: "KnifeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ammunitions_CategoryId",
                table: "Ammunitions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Knives_CategoryId",
                table: "Knives",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Ammunitions_AmmunitionId",
                table: "OrderItems",
                column: "AmmunitionId",
                principalTable: "Ammunitions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Knives_KnifeId",
                table: "OrderItems",
                column: "KnifeId",
                principalTable: "Knives",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Ammunitions_AmmunitionId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Knives_KnifeId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "Ammunitions");

            migrationBuilder.DropTable(
                name: "Knives");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_AmmunitionId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_KnifeId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "AmmunitionId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "KnifeId",
                table: "OrderItems");
        }
    }
}
