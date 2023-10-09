using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RealEstateAgency
{
    public partial class Contracts
    {
        public Contracts()
        {
            ContractServices = new HashSet<ContractServices>();
        }

        public int ContractId { get; set; }
        public DateTime? DateOfContract { get; set; }
        public int? SellerId { get; set; }
        public int? BuyerId { get; set; }
        public decimal? DealAmount { get; set; }
        public decimal? ServiceCost { get; set; }
        public string Employee { get; set; }
        public string Fiobuyer { get; set; }

        public virtual Apartments Buyer { get; set; }
        public virtual Sellers Seller { get; set; }
        public virtual ICollection<ContractServices> ContractServices { get; set; }
    }
}
