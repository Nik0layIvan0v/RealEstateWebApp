using System.Threading.Tasks;
using RealEstate.Models;

namespace RealEstate.Services
{
    public interface IBrokerService
    {
        Task<bool> IsUserAlreadyBroker(string id);

        Task AddBroker(Broker broker);

    }
}