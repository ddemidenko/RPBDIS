using System;
using System.Collections.Generic;

namespace RealEstateAgency4.Models;

public partial class Apartment
{
    public int ApartmentId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? NumberOfRooms { get; set; }

    public decimal? Area { get; set; }

    public bool? SeparateBathroom { get; set; }

    public bool? HasPhone { get; set; }

    public decimal? MaxPrice { get; set; }

    public string? AdditionalPreferences { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();
}
