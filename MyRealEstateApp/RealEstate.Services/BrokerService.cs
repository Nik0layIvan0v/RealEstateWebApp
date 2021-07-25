using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using System.Threading.Tasks;
using RealEstate.Services.Models;

namespace RealEstate.Services
{
    public class BrokerService : IBrokerService
    {
        private readonly RealEstateDbContext Context;

        public BrokerService(RealEstateDbContext context)
        {
            this.Context = context;
        }

        public async Task<bool> IsUserAlreadyBrokerAsync(string loggedUserId)
        {
            return await this.Context.Brokers.AnyAsync(broker => broker.UserId == loggedUserId);
        }

        public async Task CreateBrokerAsync(Broker broker)
        {
            await this.Context.Brokers.AddAsync(broker);

            await this.Context.SaveChangesAsync();
        }

        public async Task<int> GetBrokerIdAsync(string loggedUserId)
        {
            return await this.Context
                .Brokers
                .Where(broker => broker.UserId == loggedUserId)
                .Select(broker => broker.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MyEstateServiceModel>> GetBrokerEstatesAsync(string loggedUserId)
        {
            return await this.Context.Estates
                .Where(e => e.Broker.UserId == loggedUserId)
                .Select(estate => new MyEstateServiceModel
                {
                    Id = estate.Id,
                    FirstImage = estate.Images.First().ImageContentBytes,
                    CreatedOn = estate.CreatedOn,
                    Area = estate.Area.AreaName,
                    City = estate.City.CityName,
                    Price = estate.Price,
                    CurrencyCode = estate.Currency.CurrencyCode,
                    EstateType = estate.EstateType.TypeOfProperty,
                    TradeType = estate.TradeType.TypeOfTransaction
                })
                .ToArrayAsync();
        }
    }
}
