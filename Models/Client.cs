using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chain_pharmacies.Models;

public partial class Client
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Receipts { get; set; }

    public virtual ICollection<OrderCart> OrderCarts { get; set; } = new List<OrderCart>();

    public virtual User? User { get; set; }

    public virtual ICollection<UserCart> UserCarts { get; set; } = new List<UserCart>();
}
