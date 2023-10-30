using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class SalesMainStorage
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? NetworkId { get; set; }

    public int Quantity { get; set; }

    public DateTime SaleDate { get; set; }

    public virtual NetworkOfPharmacy? Network { get; set; }

    public virtual Product? Product { get; set; }
}
