using Lab6.Models;
using Microsoft.EntityFrameworkCore;


namespace Lab6
{
    public partial class Lab6Context : DbContext
    {
        public Lab6Context()
        {
        }

        public Lab6Context(DbContextOptions<Lab6Context> options)
            : base(options)
        {
        }


        public virtual DbSet<Contract> Contracts { get; set; }

        public virtual DbSet<ContractService> ContractServices { get; set; }
        public virtual DbSet<Service> Services { get; set; }

    }
}
