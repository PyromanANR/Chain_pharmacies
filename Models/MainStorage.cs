using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class MainStorage
{
    public int Id { get; set; }

    public string Location { get; set; } = null!;

    public int? NetworkId { get; set; }

    public virtual NetworkOfPharmacy? Network { get; set; }

    public virtual ICollection<ProductInMainStorage> ProductInMainStorages { get; set; } = new List<ProductInMainStorage>();
}
