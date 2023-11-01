using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chain_pharmacies.Migrations
{
    /// <inheritdoc />
    public partial class QNTY22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductQuantityInPack_Product_Id",
                table: "ProductQuantityInPack");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductQuantityInPack",
                table: "ProductQuantityInPack",
                column: "Product_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductQuantityInPack",
                table: "ProductQuantityInPack");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantityInPack_Product_Id",
                table: "ProductQuantityInPack",
                column: "Product_Id");
        }
    }
}
