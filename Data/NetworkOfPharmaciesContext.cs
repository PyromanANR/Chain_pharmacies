using System;
using System.Collections.Generic;
using System.Configuration;
using Chain_pharmacies.Models;
using Microsoft.EntityFrameworkCore;

namespace Chain_pharmacies.Data;

public partial class NetworkOfPharmaciesContext : DbContext
{

    public NetworkOfPharmaciesContext()
    {

    }

    public NetworkOfPharmaciesContext(DbContextOptions<NetworkOfPharmaciesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<MainAdmin> MainAdmins { get; set; }

    public virtual DbSet<MainStorage> MainStorages { get; set; }

    public virtual DbSet<NetworkOfPharmacy> NetworkOfPharmacies { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderCart> OrderCarts { get; set; }

    public virtual DbSet<OrderRequest> OrderRequests { get; set; }

    public virtual DbSet<Pharmacy> Pharmacies { get; set; }

    public virtual DbSet<PharmacyStorage> PharmacyStorages { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductInCart> ProductInCarts { get; set; }

    public virtual DbSet<ProductInMainStorage> ProductInMainStorages { get; set; }

    public virtual DbSet<ProductInStorage> ProductInStorages { get; set; }

    public virtual DbSet<ProductPriceDiscount> ProductPriceDiscounts { get; set; }

    public virtual DbSet<ProductQuantityInPack> ProductQuantityInPacks { get; set; }

    public virtual DbSet<SalesMainStorage> SalesMainStorages { get; set; }

    public virtual DbSet<SalesPharmacy> SalesPharmacies { get; set; }

    public virtual DbSet<Models.Type> Types { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCart> UserCarts { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }







    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admin__3214EC07898C6549");

            entity.ToTable("Admin");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.WorkAddress).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Admins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Admin__UserId__3F466844");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brand__3214EC07FE0CC15D");

            entity.ToTable("Brand");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC076A6DB3FA");

            entity.ToTable("Client");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PhoneNumber).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Client__UserId__3C69FB99");
        });

        modelBuilder.Entity<MainAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MainAdmi__3214EC07686100B5");

            entity.ToTable("MainAdmin");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SecretCode).HasMaxLength(50);
            entity.Property(e => e.WorkAddress).HasMaxLength(255);

            entity.HasOne(d => d.Admin).WithMany(p => p.MainAdmins)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__MainAdmin__Admin__4222D4EF");
        });

        modelBuilder.Entity<MainStorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MainStor__3214EC07BE1ED485");

            entity.ToTable("MainStorage");

            entity.Property(e => e.Location).HasMaxLength(255);

            entity.HasOne(d => d.Network).WithMany(p => p.MainStorages)
                .HasForeignKey(d => d.NetworkId)
                .HasConstraintName("FK__MainStora__Netwo__6EF57B66");
        });

        modelBuilder.Entity<NetworkOfPharmacy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NetworkO__3214EC07F94B2019");

            entity.Property(e => e.Location).HasMaxLength(255);

            entity.HasOne(d => d.MainAdmin).WithMany(p => p.NetworkOfPharmacies)
                .HasForeignKey(d => d.MainAdminId)
                .HasConstraintName("FK__NetworkOf__MainA__6C190EBB");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC072A3C8D91");

            entity.Property(e => e.CartId).HasColumnName("Cart_Id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .HasColumnName("Delivery_Address");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Total_Price");

            entity.HasOne(d => d.Cart).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__Orders__Cart_Id__5812160E");
        });

        modelBuilder.Entity<OrderCart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order_Ca__3214EC074D37A0B0");

            entity.ToTable("Order_Cart");

            entity.Property(e => e.ClientId).HasColumnName("Client_id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Total_Price");

            entity.HasOne(d => d.Client).WithMany(p => p.OrderCarts)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Order_Car__Clien__5535A963");
        });

        modelBuilder.Entity<OrderRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderReq__3214EC07F5EA253B");

            entity.Property(e => e.PharmacyId).HasColumnName("Pharmacy_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.RequestDate)
                .HasColumnType("datetime")
                .HasColumnName("Request_Date");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Pharmacy).WithMany(p => p.OrderRequests)
                .HasForeignKey(d => d.PharmacyId)
                .HasConstraintName("FK__OrderRequ__Pharm__7A672E12");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderRequests)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderRequ__Produ__797309D9");
        });

        modelBuilder.Entity<Pharmacy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pharmacy__3214EC07B160C183");

            entity.ToTable("Pharmacy");

            entity.Property(e => e.Location).HasMaxLength(255);

            entity.HasOne(d => d.Admin).WithMany(p => p.Pharmacies)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__Pharmacy__AdminI__5EBF139D");
        });

        modelBuilder.Entity<PharmacyStorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pharmacy__3214EC07AFB5B387");

            entity.ToTable("PharmacyStorage");

            entity.Property(e => e.Location).HasMaxLength(255);

            entity.HasOne(d => d.Pharmacy).WithMany(p => p.PharmacyStorages)
                .HasForeignKey(d => d.PharmacyId)
                .HasConstraintName("FK__PharmacyS__Pharm__619B8048");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07E3B82BFE");

            entity.ToTable("Product");

            entity.Property(e => e.BrandId).HasColumnName("Brand_Id");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.TypeId).HasColumnName("Type_Id");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__Product__Brand_I__48CFD27E");

            entity.HasOne(d => d.Type).WithMany(p => p.Products)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Product__Type_Id__49C3F6B7");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3214EC072758C62A");

            entity.ToTable("ProductImage");

            entity.Property(e => e.ImagePath).HasMaxLength(255);
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductIm__Produ__160F4887");
        });

        modelBuilder.Entity<ProductInCart>(entity =>
        {
            entity.HasKey(e => new { e.CartId, e.ProductId }).HasName("PK__ProductI__6F2808E2866114C7");

            entity.ToTable("ProductInCart");

            entity.Property(e => e.CartId).HasColumnName("Cart_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductInCarts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIn__Produ__4F7CD00D");
        });

        modelBuilder.Entity<ProductInMainStorage>(entity =>
        {
            entity.HasKey(e => new { e.StorageId, e.ProductId }).HasName("PK__ProductI__937E8388B638E8BC");

            entity.ToTable("ProductInMainStorage");

            entity.Property(e => e.StorageId).HasColumnName("Storage_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductInMainStorages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIn__Produ__72C60C4A");

            entity.HasOne(d => d.Storage).WithMany(p => p.ProductInMainStorages)
                .HasForeignKey(d => d.StorageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIn__Stora__71D1E811");
        });

        modelBuilder.Entity<ProductInStorage>(entity =>
        {
            entity.HasKey(e => new { e.StorageId, e.ProductId }).HasName("PK__ProductI__937E8388051D1143");

            entity.ToTable("ProductInStorage");

            entity.Property(e => e.StorageId).HasColumnName("Storage_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductInStorages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIn__Produ__656C112C");

            entity.HasOne(d => d.Storage).WithMany(p => p.ProductInStorages)
                .HasForeignKey(d => d.StorageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIn__Stora__6477ECF3");
        });

        modelBuilder.Entity<ProductPriceDiscount>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__ProductP__9834FBBA64762C25");

            entity.ToTable("ProductPriceDiscount");

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("Product_Id");
            entity.Property(e => e.Discount).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithOne(p => p.ProductPriceDiscount)
                .HasForeignKey<ProductPriceDiscount>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPr__Produ__4CA06362");
        });

        modelBuilder.Entity<ProductQuantityInPack>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductQuantityInPack");

            entity.Property(e => e.ProductId).HasColumnName("Product_Id");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductQu__Produ__17F790F9");
        });

        modelBuilder.Entity<SalesMainStorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sales_Ma__3214EC0772CEB531");

            entity.ToTable("Sales_MainStorage");

            entity.Property(e => e.NetworkId).HasColumnName("Network_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.SaleDate)
                .HasColumnType("datetime")
                .HasColumnName("Sale_Date");

            entity.HasOne(d => d.Network).WithMany(p => p.SalesMainStorages)
                .HasForeignKey(d => d.NetworkId)
                .HasConstraintName("FK__Sales_Mai__Netwo__76969D2E");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesMainStorages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Sales_Mai__Produ__75A278F5");
        });

        modelBuilder.Entity<SalesPharmacy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sales_Ph__3214EC07C6CC6B42");

            entity.ToTable("Sales_Pharmacy");

            entity.Property(e => e.PharmacyId).HasColumnName("Pharmacy_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.SaleDate)
                .HasColumnType("datetime")
                .HasColumnName("Sale_Date");

            entity.HasOne(d => d.Pharmacy).WithMany(p => p.SalesPharmacies)
                .HasForeignKey(d => d.PharmacyId)
                .HasConstraintName("FK__Sales_Pha__Pharm__693CA210");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesPharmacies)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Sales_Pha__Produ__68487DD7");
        });

        modelBuilder.Entity<Models.Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Type__3214EC07E72711B5");

            entity.ToTable("Type");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07E18ACCC4");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__398D8EEE");
        });

        modelBuilder.Entity<UserCart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User_Car__3214EC0785C6893C");

            entity.ToTable("User_Cart");

            entity.Property(e => e.ClientId).HasColumnName("Client_id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Total_Price");

            entity.HasOne(d => d.Client).WithMany(p => p.UserCarts)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__User_Cart__Clien__52593CB8");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC07F010FD73");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
