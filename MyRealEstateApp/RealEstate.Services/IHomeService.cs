using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstate.Services.Models;

namespace RealEstate.Services
{
    public interface IHomeService
    {
        Task<IEnumerable<LastAddedEstateModel>> GetLastAddedEstatesAsync(int count);

        Task<StatisticModel> GetStatistics();
    }
}