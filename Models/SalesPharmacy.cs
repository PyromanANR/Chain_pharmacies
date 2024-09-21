using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class SalesPharmacy
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? PharmacyId { get; set; }

    public int Quantity { get; set; }

    public DateTime SaleDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual Pharmacy? Pharmacy { get; set; }

    public virtual Product? Product { get; set; }
}
