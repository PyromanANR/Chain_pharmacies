using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class MainAdmin
{
    public int Id { get; set; }

    public int? AdminId { get; set; }

    public string? WorkAddress { get; set; }

    public string? SecretCode { get; set; }

    public virtual User? Admin { get; set; }

    public virtual ICollection<NetworkOfPharmacy> NetworkOfPharmacies { get; set; } = new List<NetworkOfPharmacy>();
}
