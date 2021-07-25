using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Services.Models;

namespace RealEstate.Services
{
    public class HomeService : IHomeService
    {
        private readonly RealEstateDbContext Context;

        public HomeService(RealEstateDbContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<LastAddedEstateModel>> GetLastAddedEstatesAsync(int count)
        {
            return await this.Context.Estates
                .OrderByDescending(estate => estate.CreatedOn)
                .Select(x => new LastAddedEstateModel
                {
                    Id = x.Id,
                    Squaring = x.Squaring,
                    Floor = x.Floor,
                    Description = x.Description,
                    Image = x.Images.Select(image => image.ImageContentBytes).FirstOrDefault(),
                    Price = x.Price,
                    Currency = x.Currency.CurrencyCode,
                    TradeType = x.TradeType.TypeOfTransaction,
                    Location = x.City.CityName
                })
                .Take(count)
                .ToArrayAsync();
        }

        public async Task<StatisticModel> GetStatistics()
        {
            return new StatisticModel
            {
                TotalBrokers = await this.Context.Brokers.CountAsync(),
                TotalEstates = await this.Context.Estates.CountAsync(),
                TotalUsers = await this.Context.Users.CountAsync()
            };
        }
    }
}