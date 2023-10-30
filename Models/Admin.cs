using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class Admin
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? WorkAddress { get; set; }

    public virtual ICollection<Pharmacy> Pharmacies { get; set; } = new List<Pharmacy>();

    public virtual User? User { get; set; }
}
