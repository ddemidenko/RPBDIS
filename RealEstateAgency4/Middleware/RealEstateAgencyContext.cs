﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RealEstateAgency4.Models;

public partial class RealEstateAgencyContext : IdentityDbContext
{
    public RealEstateAgencyContext()
    {
    }

    public RealEstateAgencyContext(DbContextOptions<RealEstateAgencyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<ContractService> ContractServices { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<Service> Services { get; set; }

}
