using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class OrderRequest
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? PharmacyId { get; set; }

    public int Quantity { get; set; }

    public DateTime RequestDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Pharmacy? Pharmacy { get; set; }

    public virtual Product? Product { get; set; }
}
