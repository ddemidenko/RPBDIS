using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RealEstateAgency
{
    public partial class Apartments
    {
        public Apartments()
        {
            Contracts = new HashSet<Contracts>();
            Sellers = new HashSet<Sellers>();
        }

        public int ApartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? NumberOfRooms { get; set; }
        public decimal? Area { get; set; }
        public bool? SeparateBathroom { get; set; }
        public bool? HasPhone { get; set; }
        public decimal? MaxPrice { get; set; }
        public string AdditionalPreferences { get; set; }

        public virtual ICollection<Contracts> Contracts { get; set; }
        public virtual ICollection<Sellers> Sellers { get; set; }
    }
}
