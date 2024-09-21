using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class ProductInOrder
{
    public int CartId { get; set; }

    public int OrderProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Product OrderProduct { get; set; } = null!;
}
