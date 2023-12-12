using Microsoft.Extensions.Caching.Memory;
using RealEstateAgency4.Middleware;
using RealEstateAgency4.Models;

namespace RealEstateAgency4.Services
{
    public class ApartmentsCache
    {
        private readonly RealEstateAgencyContext _context;
        private readonly IMemoryCache cache;
        public ApartmentsCache(RealEstateAgencyContext context, IMemoryCache cache)
        {
            _context = context;
            this.cache = cache;
        }

        public IEnumerable<Apartment> GetApartments()
        {
            if (!cache.TryGetValue("Apartments", out IEnumerable<Apartment> apartments))
            {
                apartments = SetApartments();
            }
            return apartments;
        }

        public IEnumerable<Apartment> SetApartments()
        {
            var apartments = _context.Apartments
        .ToList();
            
            cache.Set("Apartments", apartments, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(100000)));
            return apartments;
        }



    }
}
