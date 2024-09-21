using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class Pharmacy
{
    public int Id { get; set; }

    public string Location { get; set; } = null!;

    public int? AdminId { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual ICollection<OrderRequest> OrderRequests { get; set; } = new List<OrderRequest>();

    public virtual ICollection<PharmacyStorage> PharmacyStorages { get; set; } = new List<PharmacyStorage>();

    public virtual ICollection<SalesPharmacy> SalesPharmacies { get; set; } = new List<SalesPharmacy>();
}
