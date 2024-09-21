using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class Client
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Receipts { get; set; }

    public virtual User? User { get; set; }
}
