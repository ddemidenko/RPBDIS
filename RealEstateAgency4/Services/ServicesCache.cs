using Microsoft.Extensions.Caching.Memory;
using RealEstateAgency4.Middleware;
using RealEstateAgency4.Models;

namespace RealEstateAgency4.Services
{
    public class ServicesCache
    {
        private readonly RealEstateAgencyContext _context;
        private readonly IMemoryCache cache;
        public ServicesCache(RealEstateAgencyContext context, IMemoryCache cache)
        {
            _context = context;
            this.cache = cache;
        }

        public IEnumerable<Service> GetServices()
        {
            if (!cache.TryGetValue("Services", out IEnumerable<Service> services))
            {
                services = SetServices();
            }
            return services;
        }

        public IEnumerable<Service> SetServices()
        {
            var services = _context.Services
        .ToList();

            cache.Set("Services", services, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(100000)));
            return services;
        }



    }
}
