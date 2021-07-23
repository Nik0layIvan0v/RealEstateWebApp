using System.Threading.Tasks;
using RealEstate.Models;

namespace RealEstate.Services
{
    public interface IBrokerService
    {
        Task<bool> IsUserAlreadyBrokerAsync(string id);

        Task AddBrokerAsync(Broker broker);

    }
}