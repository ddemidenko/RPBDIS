using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RealEstateAgency
{
    public partial class ContractView
    {
        public int ContractId { get; set; }
        public DateTime? DateOfContract { get; set; }
        public string SellerFullName { get; set; }
        public string BuyerName { get; set; }
        public decimal? DealAmount { get; set; }
        public decimal? ServiceCost { get; set; }
    }
}
