using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RealEstateAgency
{
    public partial class ContractServices
    {
        public int ContractServiceId { get; set; }
        public int? ContractId { get; set; }
        public int? ServiceId { get; set; }

        public virtual Contracts Contract { get; set; }
        public virtual Services Service { get; set; }
    }
}
