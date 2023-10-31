using System;
using System.Collections.Generic;

namespace RealEstateAgencyL3.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public DateTime? DateOfContract { get; set; }

    public int? SellerId { get; set; }

    public int? BuyerId { get; set; }

    public decimal? DealAmount { get; set; }

    public decimal? ServiceCost { get; set; }

    public string? Employee { get; set; }

    public string? Fiobuyer { get; set; }

    public virtual Apartment? Buyer { get; set; }

    public virtual ICollection<ContractService> ContractServices { get; set; } = new List<ContractService>();

    public virtual Seller? Seller { get; set; }
}
