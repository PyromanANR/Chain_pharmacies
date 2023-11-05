using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chain_pharmacies.Migrations
{
    /// <inheritdoc />
    public partial class newtable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Order_Car__Clien__5535A963",
                table: "Order_Cart");

            migrationBuilder.DropForeignKey(
                name: "FK__Orders__Cart_Id__5812160E",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ClientId1",
                table: "Order_Cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_Cart_ClientId1",
                table: "Order_Cart",
                column: "ClientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Cart_Client_ClientId1",
                table: "Order_Cart",
                column: "ClientId1",
                principalTable: "Client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Order_Car__Clien__681373AD",
                table: "Order_Cart",
                column: "Client_id",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Order_Cart",
                table: "Orders",
                column: "Cart_Id",
                principalTable: "Order_Cart",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Cart_Client_ClientId1",
                table: "Order_Cart");

            migrationBuilder.DropForeignKey(
                name: "FK__Order_Car__Clien__681373AD",
                table: "Order_Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Order_Cart",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Order_Cart_ClientId1",
                table: "Order_Cart");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Order_Cart");

            migrationBuilder.AddForeignKey(
                name: "FK__Order_Car__Clien__5535A963",
                table: "Order_Cart",
                column: "Client_id",
                principalTable: "Client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Orders__Cart_Id__5812160E",
                table: "Orders",
                column: "Cart_Id",
                principalTable: "Order_Cart",
                principalColumn: "Id");
        }
    }
}
