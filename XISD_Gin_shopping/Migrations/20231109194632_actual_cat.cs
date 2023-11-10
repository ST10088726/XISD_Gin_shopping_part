using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XISD_Gin_shopping.Migrations
{
    /// <inheritdoc />
    public partial class actual_cat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Stock",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stock_CategoryId",
                table: "Stock",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Category_CategoryId",
                table: "Stock",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Category_CategoryId",
                table: "Stock");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Stock_CategoryId",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Stock");
        }
    }
}
