using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public class BrokerService : IBrokerService
    {
        private readonly RealEstateDbContext Context;

        public BrokerService(RealEstateDbContext context)
        {
            Context = context;
        }

        public async Task<bool> IsUserAlreadyBrokerAsync(string loggedUserId)
        {
            return await this.Context.Brokers.AnyAsync(x => x.UserId == loggedUserId);
        }

        public async Task AddBrokerAsync(Broker broker)
        {
            await this.Context.Brokers.AddAsync(broker);

            await this.Context.SaveChangesAsync();
        }
    }
}
