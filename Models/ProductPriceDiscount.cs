using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class ProductPriceDiscount
{
    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public decimal? Discount { get; set; }

    public virtual Product Product { get; set; } = null!;

    public decimal GetDiscountedPrice()
    {
        decimal discountPercent = Discount ?? 0;
        decimal discountAmount = Price * discountPercent;
        decimal discountedPrice = Price - discountAmount;

        // Round discountedPrice to 2 decimal places
        discountedPrice = Math.Round(discountedPrice, 2);

        return discountedPrice;
    }




}
