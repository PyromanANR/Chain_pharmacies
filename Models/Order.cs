using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int? CartId { get; set; }

    public decimal TotalPrice { get; set; }

    public string? DeliveryAddress { get; set; }

    public virtual OrderCart? Cart { get; set; }
}
