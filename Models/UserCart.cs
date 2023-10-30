using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class UserCart
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public DateTime Date { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Client? Client { get; set; }
}
