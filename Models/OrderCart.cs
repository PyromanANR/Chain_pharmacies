using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class OrderCart
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public DateTime Date { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
