using System;
using System.Collections.Generic;

namespace RealEstateAgency4.Models;

public partial class Seller
{
    public int SellerId { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? PassportData { get; set; }

    public int? ApartmentId { get; set; }

    public string? ApartmentAddress { get; set; }

    public decimal? Price { get; set; }

    public string? AdditionalInformation { get; set; }

    public virtual Apartment? Apartment { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
