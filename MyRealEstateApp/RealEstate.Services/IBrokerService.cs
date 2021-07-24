using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstate.Models;
using RealEstate.Services.Models;

namespace RealEstate.Services
{
    public interface IBrokerService
    {
        Task<bool> IsUserAlreadyBrokerAsync(string id);

        Task AddBrokerAsync(Broker broker);

        Task<IEnumerable<MyEstateServiceModel>> GetMyEstatesAsync(string userId);
    }
}