using System;
using System.Collections.Generic;

namespace RealEstateAgencyL3.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<ContractService> ContractServices { get; set; } = new List<ContractService>();
}
