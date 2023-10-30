using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class NetworkOfPharmacy
{
    public int Id { get; set; }

    public string Location { get; set; } = null!;

    public int? MainAdminId { get; set; }

    public virtual MainAdmin? MainAdmin { get; set; }

    public virtual ICollection<MainStorage> MainStorages { get; set; } = new List<MainStorage>();

    public virtual ICollection<SalesMainStorage> SalesMainStorages { get; set; } = new List<SalesMainStorage>();
}
