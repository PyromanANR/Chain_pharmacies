using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class ProductPriceDiscount
{
    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public decimal? Discount { get; set; }

    public virtual Product Product { get; set; } = null!;
}
