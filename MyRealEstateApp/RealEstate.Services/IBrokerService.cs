using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstate.Models;
using RealEstate.Services.Models;

namespace RealEstate.Services
{
    public interface IBrokerService
    {
        Task<int> GetBrokerIdAsync(string loggedUserId);

        Task<bool> IsUserAlreadyBrokerAsync(string loggedUserId);

        Task CreateBrokerAsync(Broker broker);

        Task<IEnumerable<MyEstateServiceModel>> GetBrokerEstatesAsync(string loggedUserId);
    }
}