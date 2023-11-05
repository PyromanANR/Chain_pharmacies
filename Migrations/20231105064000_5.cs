using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chain_pharmacies.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Sales_Pharmacy",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Sales_MainStorage",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Sales_Pharmacy");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Sales_MainStorage");
        }
    }
}
