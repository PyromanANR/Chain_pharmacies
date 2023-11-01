using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class Product
{
    public int Id { get; set; }

    public int? BrandId { get; set; }

    public int? TypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<OrderRequest> OrderRequests { get; set; } = new List<OrderRequest>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductInCart> ProductInCarts { get; set; } = new List<ProductInCart>();

    public virtual ICollection<ProductInMainStorage> ProductInMainStorages { get; set; } = new List<ProductInMainStorage>();

    public virtual ICollection<ProductInStorage> ProductInStorages { get; set; } = new List<ProductInStorage>();

    public virtual ProductPriceDiscount? ProductPriceDiscount { get; set; }

    public virtual ProductQuantityInPack? ProductQuantityInPack { get; set; }

    public virtual ICollection<SalesMainStorage> SalesMainStorages { get; set; } = new List<SalesMainStorage>();

    public virtual ICollection<SalesPharmacy> SalesPharmacies { get; set; } = new List<SalesPharmacy>();

    public virtual Type? Type { get; set; }
}
