using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RealEstateAgency
{
    public partial class Services
    {
        public Services()
        {
            ContractServices = new HashSet<ContractServices>();
        }

        public int ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<ContractServices> ContractServices { get; set; }
    }
}
