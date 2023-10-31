using System.Threading.Tasks;
using RealEstateAgencyL3.Models;

namespace RealEstateAgencyL3.Services
{
    public interface ICacheContractsService
    {
        public IEnumerable<Contract> GetContracts(int rowsNumber = 20);
        public void AddContracts(string cacheKey, int rowsNumber = 20);
        public IEnumerable<Contract> GetContracts(string cacheKey, int rowsNumber = 20);
        public IEnumerable<Contract> GetContractById(int? contractId, int rowsNumber = 20);
    }
}
