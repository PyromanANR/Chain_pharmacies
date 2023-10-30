using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class ProductInCart
{
    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;
}
