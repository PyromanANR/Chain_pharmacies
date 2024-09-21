using System;
using System.Collections.Generic;

namespace Chain_pharmacies.Models;

public partial class Table2
{
    public int Id { get; set; }

    public string? ClientName { get; set; }

    public string? NamePharmacy { get; set; }

    public DateTime? DataTimeScript { get; set; }
}
