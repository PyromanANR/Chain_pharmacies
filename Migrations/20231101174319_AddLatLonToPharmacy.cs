using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chain_pharmacies.Migrations
{
    /// <inheritdoc />
    public partial class AddLatLonToPharmacy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Brand__3214EC07FE0CC15D", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Type__3214EC07E72711B5", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRole__3214EC07F010FD73", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand_Id = table.Column<int>(type: "int", nullable: true),
                    Type_Id = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__3214EC07E3B82BFE", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Product__Brand_I__48CFD27E",
                        column: x => x.Brand_Id,
                        principalTable: "Brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Product__Type_Id__49C3F6B7",
                        column: x => x.Type_Id,
                        principalTable: "Type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3214EC07E18ACCC4", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Users__RoleId__398D8EEE",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Id = table.Column<int>(type: "int", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductI__3214EC072758C62A", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ProductIm__Produ__160F4887",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductInCart",
                columns: table => new
                {
                    Cart_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductI__6F2808E2866114C7", x => new { x.Cart_Id, x.Product_Id });
                    table.ForeignKey(
                        name: "FK__ProductIn__Produ__4F7CD00D",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductPriceDiscount",
                columns: table => new
                {
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(3,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductP__9834FBBA64762C25", x => x.Product_Id);
                    table.ForeignKey(
                        name: "FK__ProductPr__Produ__4CA06362",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductQuantityInPack",
                columns: table => new
                {
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__ProductQu__Produ__17F790F9",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    WorkAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Admin__3214EC07898C6549", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Admin__UserId__3F466844",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Receipts = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Client__3214EC076A6DB3FA", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Client__UserId__3C69FB99",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MainAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    WorkAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SecretCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MainAdmi__3214EC07686100B5", x => x.Id);
                    table.ForeignKey(
                        name: "FK__MainAdmin__Admin__4222D4EF",
                        column: x => x.AdminId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pharmacy__3214EC07B160C183", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Pharmacy__AdminI__5EBF139D",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Order_Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_id = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Total_Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order_Ca__3214EC074D37A0B0", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Order_Car__Clien__5535A963",
                        column: x => x.Client_id,
                        principalTable: "Client",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User_Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_id = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Total_Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Car__3214EC0785C6893C", x => x.Id);
                    table.ForeignKey(
                        name: "FK__User_Cart__Clien__52593CB8",
                        column: x => x.Client_id,
                        principalTable: "Client",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NetworkOfPharmacies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MainAdminId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NetworkO__3214EC07F94B2019", x => x.Id);
                    table.ForeignKey(
                        name: "FK__NetworkOf__MainA__6C190EBB",
                        column: x => x.MainAdminId,
                        principalTable: "MainAdmin",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Id = table.Column<int>(type: "int", nullable: true),
                    Pharmacy_Id = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Request_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderReq__3214EC07F5EA253B", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OrderRequ__Pharm__7A672E12",
                        column: x => x.Pharmacy_Id,
                        principalTable: "Pharmacy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__OrderRequ__Produ__797309D9",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PharmacyStorage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pharmacy__3214EC07AFB5B387", x => x.Id);
                    table.ForeignKey(
                        name: "FK__PharmacyS__Pharm__619B8048",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sales_Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Id = table.Column<int>(type: "int", nullable: true),
                    Pharmacy_Id = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Sale_Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sales_Ph__3214EC07C6CC6B42", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Sales_Pha__Pharm__693CA210",
                        column: x => x.Pharmacy_Id,
                        principalTable: "Pharmacy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Sales_Pha__Produ__68487DD7",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Cart_Id = table.Column<int>(type: "int", nullable: true),
                    Total_Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Delivery_Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__3214EC072A3C8D91", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Orders__Cart_Id__5812160E",
                        column: x => x.Cart_Id,
                        principalTable: "Order_Cart",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MainStorage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NetworkId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MainStor__3214EC07BE1ED485", x => x.Id);
                    table.ForeignKey(
                        name: "FK__MainStora__Netwo__6EF57B66",
                        column: x => x.NetworkId,
                        principalTable: "NetworkOfPharmacies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sales_MainStorage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Id = table.Column<int>(type: "int", nullable: true),
                    Network_Id = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Sale_Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sales_Ma__3214EC0772CEB531", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Sales_Mai__Netwo__76969D2E",
                        column: x => x.Network_Id,
                        principalTable: "NetworkOfPharmacies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Sales_Mai__Produ__75A278F5",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductInStorage",
                columns: table => new
                {
                    Storage_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductI__937E8388051D1143", x => new { x.Storage_Id, x.Product_Id });
                    table.ForeignKey(
                        name: "FK__ProductIn__Produ__656C112C",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__ProductIn__Stora__6477ECF3",
                        column: x => x.Storage_Id,
                        principalTable: "PharmacyStorage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductInMainStorage",
                columns: table => new
                {
                    Storage_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductI__937E8388B638E8BC", x => new { x.Storage_Id, x.Product_Id });
                    table.ForeignKey(
                        name: "FK__ProductIn__Produ__72C60C4A",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__ProductIn__Stora__71D1E811",
                        column: x => x.Storage_Id,
                        principalTable: "MainStorage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UserId",
                table: "Admin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                table: "Client",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MainAdmin_AdminId",
                table: "MainAdmin",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_MainStorage_NetworkId",
                table: "MainStorage",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkOfPharmacies_MainAdminId",
                table: "NetworkOfPharmacies",
                column: "MainAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Cart_Client_id",
                table: "Order_Cart",
                column: "Client_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRequests_Pharmacy_Id",
                table: "OrderRequests",
                column: "Pharmacy_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRequests_Product_Id",
                table: "OrderRequests",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Cart_Id",
                table: "Orders",
                column: "Cart_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacy_AdminId",
                table: "Pharmacy",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyStorage_PharmacyId",
                table: "PharmacyStorage",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Brand_Id",
                table: "Product",
                column: "Brand_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Type_Id",
                table: "Product",
                column: "Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_Product_Id",
                table: "ProductImage",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCart_Product_Id",
                table: "ProductInCart",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInMainStorage_Product_Id",
                table: "ProductInMainStorage",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInStorage_Product_Id",
                table: "ProductInStorage",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantityInPack_Product_Id",
                table: "ProductQuantityInPack",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_MainStorage_Network_Id",
                table: "Sales_MainStorage",
                column: "Network_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_MainStorage_Product_Id",
                table: "Sales_MainStorage",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_Pharmacy_Pharmacy_Id",
                table: "Sales_Pharmacy",
                column: "Pharmacy_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_Pharmacy_Product_Id",
                table: "Sales_Pharmacy",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Cart_Client_id",
                table: "User_Cart",
                column: "Client_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRequests");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "ProductInCart");

            migrationBuilder.DropTable(
                name: "ProductInMainStorage");

            migrationBuilder.DropTable(
                name: "ProductInStorage");

            migrationBuilder.DropTable(
                name: "ProductPriceDiscount");

            migrationBuilder.DropTable(
                name: "ProductQuantityInPack");

            migrationBuilder.DropTable(
                name: "Sales_MainStorage");

            migrationBuilder.DropTable(
                name: "Sales_Pharmacy");

            migrationBuilder.DropTable(
                name: "User_Cart");

            migrationBuilder.DropTable(
                name: "Order_Cart");

            migrationBuilder.DropTable(
                name: "MainStorage");

            migrationBuilder.DropTable(
                name: "PharmacyStorage");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "NetworkOfPharmacies");

            migrationBuilder.DropTable(
                name: "Pharmacy");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropTable(
                name: "MainAdmin");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
