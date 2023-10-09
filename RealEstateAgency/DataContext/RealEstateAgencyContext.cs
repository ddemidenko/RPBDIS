using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RealEstateAgency
{
    public partial class RealEstateAgencyContext : DbContext
    {
        public RealEstateAgencyContext()
        {
        }

        public RealEstateAgencyContext(DbContextOptions<RealEstateAgencyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApartmentView> ApartmentView { get; set; }
        public virtual DbSet<Apartments> Apartments { get; set; }
        public virtual DbSet<ContractServices> ContractServices { get; set; }
        public virtual DbSet<ContractView> ContractView { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<Sellers> Sellers { get; set; }
        public virtual DbSet<ServiceView> ServiceView { get; set; }
        public virtual DbSet<Services> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=RealEstateAgency;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApartmentView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ApartmentView");

                entity.Property(e => e.AdditionalPreferences).HasMaxLength(1000);

                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.Area).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.MaxPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Apartments>(entity =>
            {
                entity.HasKey(e => e.ApartmentId)
                    .HasName("PK__Apartmen__CBDF57444B3534CB");

                entity.Property(e => e.ApartmentId)
                    .HasColumnName("ApartmentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdditionalPreferences).HasMaxLength(1000);

                entity.Property(e => e.Area).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.MaxPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<ContractServices>(entity =>
            {
                entity.HasKey(e => e.ContractServiceId)
                    .HasName("PK__Contract__55E1AE33F404A6CB");

                entity.Property(e => e.ContractServiceId)
                    .HasColumnName("ContractServiceID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractServices)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK__ContractS__Contr__412EB0B6");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ContractServices)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__ContractS__Servi__4222D4EF");
            });

            modelBuilder.Entity<ContractView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ContractView");

                entity.Property(e => e.BuyerName).HasMaxLength(255);

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.DateOfContract).HasColumnType("date");

                entity.Property(e => e.DealAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.SellerFullName).HasMaxLength(255);

                entity.Property(e => e.ServiceCost).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.HasKey(e => e.ContractId)
                    .HasName("PK__Contract__C90D3409C859D437");

                entity.Property(e => e.ContractId)
                    .HasColumnName("ContractID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BuyerId).HasColumnName("BuyerID");

                entity.Property(e => e.DateOfContract).HasColumnType("date");

                entity.Property(e => e.DealAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Employee).HasMaxLength(255);

                entity.Property(e => e.Fiobuyer)
                    .HasColumnName("FIOBuyer")
                    .HasMaxLength(255);

                entity.Property(e => e.SellerId).HasColumnName("SellerID");

                entity.Property(e => e.ServiceCost).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__Contracts__Buyer__3C69FB99");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__Contracts__Selle__3B75D760");
            });

            modelBuilder.Entity<Sellers>(entity =>
            {
                entity.HasKey(e => e.SellerId)
                    .HasName("PK__Sellers__7FE3DBA12300466D");

                entity.Property(e => e.SellerId)
                    .HasColumnName("SellerID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdditionalInformation).HasMaxLength(1000);

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.ApartmentAddress).HasMaxLength(500);

                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.PassportData).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Sellers)
                    .HasForeignKey(d => d.ApartmentId)
                    .HasConstraintName("FK__Sellers__Apartme__38996AB5");
            });

            modelBuilder.Entity<ServiceView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ServiceView");

                entity.Property(e => e.ServiceDescription).HasMaxLength(1000);

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.ServiceName).HasMaxLength(255);

                entity.Property(e => e.ServicePrice).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Services>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK__Services__C51BB0EAFDD30054");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("ServiceID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
