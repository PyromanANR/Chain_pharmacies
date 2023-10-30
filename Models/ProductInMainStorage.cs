﻿using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class ProductInMainStorage
{
    public int StorageId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual MainStorage Storage { get; set; } = null!;
}
