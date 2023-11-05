using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chain_pharmacies.Migrations
{
    /// <inheritdoc />
    public partial class _4124124 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Cart_Client_ClientId1",
                table: "Order_Cart");

            migrationBuilder.DropForeignKey(
                name: "FK__Order_Car__Clien__681373AD",
                table: "Order_Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Order_Ca__3214EC074D37A0B0",
                table: "Order_Cart");

            migrationBuilder.RenameTable(
                name: "Order_Cart",
                newName: "OrderCarts");

            migrationBuilder.RenameColumn(
                name: "Total_Price",
                table: "OrderCarts",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "Client_id",
                table: "OrderCarts",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "ClientId1",
                table: "OrderCarts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Cart_ClientId1",
                table: "OrderCarts",
                newName: "IX_OrderCarts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Cart_Client_id",
                table: "OrderCarts",
                newName: "IX_OrderCarts_ClientId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "OrderCarts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "OrderCarts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderCarts",
                table: "OrderCarts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCarts_Client_ClientId",
                table: "OrderCarts",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderCarts_Users_UserId",
                table: "OrderCarts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderCarts_Client_ClientId",
                table: "OrderCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderCarts_Users_UserId",
                table: "OrderCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderCarts",
                table: "OrderCarts");

            migrationBuilder.RenameTable(
                name: "OrderCarts",
                newName: "Order_Cart");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Order_Cart",
                newName: "Total_Price");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Order_Cart",
                newName: "Client_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order_Cart",
                newName: "ClientId1");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCarts_UserId",
                table: "Order_Cart",
                newName: "IX_Order_Cart_ClientId1");

            migrationBuilder.RenameIndex(
                name: "IX_OrderCarts_ClientId",
                table: "Order_Cart",
                newName: "IX_Order_Cart_Client_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Order_Cart",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total_Price",
                table: "Order_Cart",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Order_Ca__3214EC074D37A0B0",
                table: "Order_Cart",
                column: "Id");

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
        }
    }
}
