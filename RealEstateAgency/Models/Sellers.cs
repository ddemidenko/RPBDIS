using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RealEstateAgency
{
    public partial class Sellers
    {
        public Sellers()
        {
            Contracts = new HashSet<Contracts>();
        }

        public int SellerId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PassportData { get; set; }
        public int? ApartmentId { get; set; }
        public string ApartmentAddress { get; set; }
        public decimal? Price { get; set; }
        public string AdditionalInformation { get; set; }

        public virtual Apartments Apartment { get; set; }
        public virtual ICollection<Contracts> Contracts { get; set; }
    }
}
