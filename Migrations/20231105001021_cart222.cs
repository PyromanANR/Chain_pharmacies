using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chain_pharmacies.Migrations
{
    /// <inheritdoc />
    public partial class cart222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__User_Cart__Clien__52593CB8",
                table: "User_Cart");

            migrationBuilder.AddForeignKey(
                name: "FK__User_Cart__Clien__625A9A57",
                table: "User_Cart",
                column: "Client_id",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__User_Cart__Clien__625A9A57",
                table: "User_Cart");

            migrationBuilder.AddForeignKey(
                name: "FK__User_Cart__Clien__52593CB8",
                table: "User_Cart",
                column: "Client_id",
                principalTable: "Client",
                principalColumn: "Id");
        }
    }
}
