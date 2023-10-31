using Microsoft.Extensions.Caching.Memory;
using RealEstateAgencyL3.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RealEstateAgencyL3.Models;
using System.Linq;
using System.Net;

namespace RealEstateAgencyL3.Services
{
    public class CacheContractsService : ICacheContractsService
    {
        private readonly RealEstateAgencyContext _dbContext;
        private readonly IMemoryCache _memoryCache;
        private int time = 2 * 9 * 240;

        public CacheContractsService(RealEstateAgencyContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }
        public IEnumerable<Contract> GetContracts(int rowsNumber = 20)
        {
            return _dbContext.Contracts.Take(rowsNumber).ToList();
        }
        public void AddContracts(string cacheKey, int rowsNumber = 20)
        {
            IEnumerable<Contract> Contracts = _dbContext.Contracts.Take(rowsNumber).ToList();
            if (Contracts != null)
            {
                _memoryCache.Set(cacheKey, Contracts, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(time)
                });

            }

        }
        public IEnumerable<Contract> GetContracts(string cacheKey, int rowsNumber = 20)
        {
            IEnumerable<Contract> Contracts;
            if (!_memoryCache.TryGetValue(cacheKey, out Contracts))
            {
                Contracts = _dbContext.Contracts.Take(rowsNumber).ToList();
                if (Contracts != null)
                {
                    _memoryCache.Set(cacheKey, Contracts,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(time)));
                }
            }
            return Contracts;
        }

        public IEnumerable<Contract> GetContractById(int? ContractId, int rowsNumber = 20)
        {
            return _dbContext.Contracts
                .Where(contract => contract.ContractId == ContractId)
                .ToList();
        }
    }
}
