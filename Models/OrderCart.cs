using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class OrderCart
{
    public int Id { get; set; }

    public int? OrderClientId { get; set; }

    public DateTime Date { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual User? OrderClient { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
