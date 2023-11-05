﻿// <auto-generated />
using System;
using Chain_pharmacies.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chain_pharmacies.Migrations
{
    [DbContext(typeof(NetworkOfPharmaciesContext))]
    [Migration("20231105063805_4")]
    partial class _4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Chain_pharmacies.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("WorkAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Brand__3214EC07FE0CC15D");

                    b.ToTable("Brand", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Receipts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.MainAdmin", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("SecretCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("WorkAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__MainAdmi__3214EC07686100B5");

                    b.HasIndex("AdminId");

                    b.ToTable("MainAdmin", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.MainStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("NetworkId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__MainStor__3214EC07BE1ED485");

                    b.HasIndex("NetworkId");

                    b.ToTable("MainStorage", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.NetworkOfPharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("MainAdminId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__NetworkO__3214EC07F94B2019");

                    b.HasIndex("MainAdminId");

                    b.ToTable("NetworkOfPharmacies");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CartId")
                        .HasColumnType("int")
                        .HasColumnName("Cart_Id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("DeliveryAddress")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Delivery_Address");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("Total_Price");

                    b.HasKey("Id")
                        .HasName("PK__Orders__3214EC072A3C8D91");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.OrderRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int")
                        .HasColumnName("Pharmacy_Id");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("Product_Id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Request_Date");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__OrderReq__3214EC07F5EA253B");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderRequests");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Pharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("Id")
                        .HasName("PK__Pharmacy__3214EC07B160C183");

                    b.HasIndex("AdminId");

                    b.ToTable("Pharmacy", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.PharmacyStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Pharmacy__3214EC07AFB5B387");

                    b.HasIndex("PharmacyId");

                    b.ToTable("PharmacyStorage", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int")
                        .HasColumnName("Brand_Id");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("Type_Id");

                    b.HasKey("Id")
                        .HasName("PK__Product__3214EC07E3B82BFE");

                    b.HasIndex("BrandId");

                    b.HasIndex("TypeId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("Product_Id");

                    b.HasKey("Id")
                        .HasName("PK__ProductI__3214EC072758C62A");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductInCart", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int")
                        .HasColumnName("Cart_Id");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("Product_Id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartId", "ProductId")
                        .HasName("PK__ProductI__6F2808E2866114C7");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInCart", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductInMainStorage", b =>
                {
                    b.Property<int>("StorageId")
                        .HasColumnType("int")
                        .HasColumnName("Storage_Id");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("Product_Id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("StorageId", "ProductId")
                        .HasName("PK__ProductI__937E8388B638E8BC");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInMainStorage", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductInOrder", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int")
                        .HasColumnName("Cart_Id");

                    b.Property<int>("OrderProductId")
                        .HasColumnType("int")
                        .HasColumnName("Order_Product_Id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartId", "OrderProductId")
                        .HasName("PK__ProductI__F0F0AE599611080C");

                    b.HasIndex("OrderProductId");

                    b.ToTable("ProductInOrder", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductInStorage", b =>
                {
                    b.Property<int>("StorageId")
                        .HasColumnType("int")
                        .HasColumnName("Storage_Id");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("Product_Id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("StorageId", "ProductId")
                        .HasName("PK__ProductI__937E8388051D1143");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInStorage", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductPriceDiscount", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("Product_Id");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(3, 2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("ProductId")
                        .HasName("PK__ProductP__9834FBBA64762C25");

                    b.ToTable("ProductPriceDiscount", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductQuantityInPack", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("Product_Id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("ProductQuantityInPack", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.SalesMainStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("NetworkId")
                        .HasColumnType("int")
                        .HasColumnName("Network_Id");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("Product_Id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Sale_Date");

                    b.HasKey("Id")
                        .HasName("PK__Sales_Ma__3214EC0772CEB531");

                    b.HasIndex("NetworkId");

                    b.HasIndex("ProductId");

                    b.ToTable("Sales_MainStorage", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.SalesPharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int")
                        .HasColumnName("Pharmacy_Id");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("Product_Id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Sale_Date");

                    b.HasKey("Id")
                        .HasName("PK__Sales_Ph__3214EC07C6CC6B42");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("ProductId");

                    b.ToTable("Sales_Pharmacy", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Type__3214EC07E72711B5");

                    b.ToTable("Type", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Users__3214EC07E18ACCC4");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.UserCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("Client_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("Total_Price");

                    b.HasKey("Id")
                        .HasName("PK__User_Car__3214EC07B280D8F5");

                    b.HasIndex("ClientId");

                    b.ToTable("User_Cart", (string)null);
                });

            modelBuilder.Entity("Chain_pharmacies.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__UserRole__3214EC07F010FD73");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Admin", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.User", "User")
                        .WithMany("Admins")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Tmp_Admin__UserI__3B40CD36");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Client", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.User", "User")
                        .WithMany("Clients")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Tmp_Clien__UserI__3493CFA7");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.MainAdmin", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.User", "Admin")
                        .WithMany("MainAdmins")
                        .HasForeignKey("AdminId")
                        .HasConstraintName("FK__MainAdmin__Admin__4222D4EF");

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.MainStorage", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.NetworkOfPharmacy", "Network")
                        .WithMany("MainStorages")
                        .HasForeignKey("NetworkId")
                        .HasConstraintName("FK__MainStora__Netwo__6EF57B66");

                    b.Navigation("Network");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.NetworkOfPharmacy", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.MainAdmin", "MainAdmin")
                        .WithMany("NetworkOfPharmacies")
                        .HasForeignKey("MainAdminId")
                        .HasConstraintName("FK__NetworkOf__MainA__6C190EBB");

                    b.Navigation("MainAdmin");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.OrderRequest", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Pharmacy", "Pharmacy")
                        .WithMany("OrderRequests")
                        .HasForeignKey("PharmacyId")
                        .HasConstraintName("FK__OrderRequ__Pharm__7A672E12");

                    b.HasOne("Chain_pharmacies.Models.Product", "Product")
                        .WithMany("OrderRequests")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__OrderRequ__Produ__797309D9");

                    b.Navigation("Pharmacy");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Pharmacy", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Admin", "Admin")
                        .WithMany("Pharmacies")
                        .HasForeignKey("AdminId")
                        .HasConstraintName("FK__Pharmacy__AdminI__5EBF139D");

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.PharmacyStorage", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Pharmacy", "Pharmacy")
                        .WithMany("PharmacyStorages")
                        .HasForeignKey("PharmacyId")
                        .HasConstraintName("FK__PharmacyS__Pharm__619B8048");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Product", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .HasConstraintName("FK__Product__Brand_I__48CFD27E");

                    b.HasOne("Chain_pharmacies.Models.Type", "Type")
                        .WithMany("Products")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK__Product__Type_Id__49C3F6B7");

                    b.Navigation("Brand");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductImage", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__ProductIm__Produ__160F4887");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductInCart", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Product", "Product")
                        .WithMany("ProductInCarts")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductIn__Produ__4F7CD00D");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductInMainStorage", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Product", "Product")
                        .WithMany("ProductInMainStorages")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductIn__Produ__72C60C4A");

                    b.HasOne("Chain_pharmacies.Models.MainStorage", "Storage")
                        .WithMany("ProductInMainStorages")
                        .HasForeignKey("StorageId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductIn__Stora__71D1E811");

                    b.Navigation("Product");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductInOrder", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Product", "OrderProduct")
                        .WithMany("ProductInOrders")
                        .HasForeignKey("OrderProductId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductIn__Order__7A3223E8");

                    b.Navigation("OrderProduct");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductInStorage", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Product", "Product")
                        .WithMany("ProductInStorages")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductIn__Produ__656C112C");

                    b.HasOne("Chain_pharmacies.Models.PharmacyStorage", "Storage")
                        .WithMany("ProductInStorages")
                        .HasForeignKey("StorageId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductIn__Stora__6477ECF3");

                    b.Navigation("Product");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductPriceDiscount", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Product", "Product")
                        .WithOne("ProductPriceDiscount")
                        .HasForeignKey("Chain_pharmacies.Models.ProductPriceDiscount", "ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductPr__Produ__4CA06362");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.ProductQuantityInPack", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Product", "Product")
                        .WithOne("ProductQuantityInPack")
                        .HasForeignKey("Chain_pharmacies.Models.ProductQuantityInPack", "ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductQu__Produ__17F790F9");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.SalesMainStorage", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.NetworkOfPharmacy", "Network")
                        .WithMany("SalesMainStorages")
                        .HasForeignKey("NetworkId")
                        .HasConstraintName("FK__Sales_Mai__Netwo__76969D2E");

                    b.HasOne("Chain_pharmacies.Models.Product", "Product")
                        .WithMany("SalesMainStorages")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__Sales_Mai__Produ__75A278F5");

                    b.Navigation("Network");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.SalesPharmacy", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.Pharmacy", "Pharmacy")
                        .WithMany("SalesPharmacies")
                        .HasForeignKey("PharmacyId")
                        .HasConstraintName("FK__Sales_Pha__Pharm__693CA210");

                    b.HasOne("Chain_pharmacies.Models.Product", "Product")
                        .WithMany("SalesPharmacies")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__Sales_Pha__Produ__68487DD7");

                    b.Navigation("Pharmacy");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.User", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.UserRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__Users__RoleId__398D8EEE");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.UserCart", b =>
                {
                    b.HasOne("Chain_pharmacies.Models.User", "Client")
                        .WithMany("UserCarts")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK__User_Cart__Clien__625A9A57");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Admin", b =>
                {
                    b.Navigation("Pharmacies");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.MainAdmin", b =>
                {
                    b.Navigation("NetworkOfPharmacies");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.MainStorage", b =>
                {
                    b.Navigation("ProductInMainStorages");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.NetworkOfPharmacy", b =>
                {
                    b.Navigation("MainStorages");

                    b.Navigation("SalesMainStorages");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Pharmacy", b =>
                {
                    b.Navigation("OrderRequests");

                    b.Navigation("PharmacyStorages");

                    b.Navigation("SalesPharmacies");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.PharmacyStorage", b =>
                {
                    b.Navigation("ProductInStorages");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Product", b =>
                {
                    b.Navigation("OrderRequests");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductInCarts");

                    b.Navigation("ProductInMainStorages");

                    b.Navigation("ProductInOrders");

                    b.Navigation("ProductInStorages");

                    b.Navigation("ProductPriceDiscount");

                    b.Navigation("ProductQuantityInPack");

                    b.Navigation("SalesMainStorages");

                    b.Navigation("SalesPharmacies");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.Type", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.User", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Clients");

                    b.Navigation("MainAdmins");

                    b.Navigation("UserCarts");
                });

            modelBuilder.Entity("Chain_pharmacies.Models.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
