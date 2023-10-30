using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class PharmacyStorage
{
    public int Id { get; set; }

    public string Location { get; set; } = null!;

    public int? PharmacyId { get; set; }

    public virtual Pharmacy? Pharmacy { get; set; }

    public virtual ICollection<ProductInStorage> ProductInStorages { get; set; } = new List<ProductInStorage>();
}
