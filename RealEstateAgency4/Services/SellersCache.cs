using Microsoft.Extensions.Caching.Memory;
using RealEstateAgency4.Middleware;
using RealEstateAgency4.Models;

namespace RealEstateAgency4.Services
{
    public class SellersCache
    {
        private readonly RealEstateAgencyContext _context;
        private readonly IMemoryCache cache;
        public SellersCache(RealEstateAgencyContext context, IMemoryCache cache)
        {
            _context = context;
            this.cache = cache;
        }

        public IEnumerable<Seller> GetSellers()
        {
            if (!cache.TryGetValue("Sellers", out IEnumerable<Seller> sellers))
            {
                sellers = SetSellers();
            }
            return sellers;
        }

        public IEnumerable<Seller> SetSellers()
        {
            var sellers = _context.Sellers
        .ToList();
            foreach (var seller in sellers)
            {

                _context.Entry(seller).Reference(l => l.Apartment).Load();

            }
            cache.Set("Sellers", sellers, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(100000)));
            return sellers;
        }



    }
}
