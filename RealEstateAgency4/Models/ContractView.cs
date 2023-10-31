using System;
using System.Collections.Generic;

namespace RealEstateAgency4.Models;

public partial class ContractView
{
    public int ContractId { get; set; }

    public DateTime? DateOfContract { get; set; }

    public string? SellerFullName { get; set; }

    public string? BuyerName { get; set; }

    public decimal? DealAmount { get; set; }

    public decimal? ServiceCost { get; set; }
}
