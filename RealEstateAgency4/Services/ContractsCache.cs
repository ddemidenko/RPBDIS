using Microsoft.Extensions.Caching.Memory;
using RealEstateAgency4.Middleware;
using RealEstateAgency4.Models;

namespace RealEstateAgency4.Services
{
    public class ContractsCache
    {
        private readonly RealEstateAgencyContext _context;
        private readonly IMemoryCache cache;
        public ContractsCache(RealEstateAgencyContext context, IMemoryCache cache)
        {
            _context = context;
            this.cache = cache;
        }

        public IEnumerable<Contract> GetContracts()
        {
            if (!cache.TryGetValue("Contracts", out IEnumerable<Contract> contracts))
            {
                contracts = SetContracts();
            }
            return contracts;
        }

        public IEnumerable<Contract> SetContracts()
        {
            var contracts = _context.Contracts
        .ToList();
            foreach (var contract in contracts)
            {

                _context.Entry(contract).Reference(l => l.Seller).Load();
     
            }
            cache.Set("Contracts", contracts, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(100000)));
            return contracts;
        }



    }
}
