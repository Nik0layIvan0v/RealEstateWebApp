using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public class EstateService : IEstateService
    {
        private readonly RealEstateDbContext Context;

        public EstateService(RealEstateDbContext context)
        {
            this.Context = context;
        }

        public CreateEstateDropDownModel GetDropDownData()
        {
            ICollection<AreaModel> areaModels = this.Context.Areas
                .Select(x => new AreaModel
                {
                    Id = x.Id,
                    Area = x.AreaName
                })
                .ToArray();

            ICollection<EstateTypeModel> estateTypeModels = this.Context.EstateTypes
                .Select(x => new EstateTypeModel
                {
                    Id = x.Id,
                    Type = x.TypeOfProperty
                })
                .ToArray();

            IEnumerable<CurrencyModel> currencyModels = this.Context.Currencies
                .Select(x => new CurrencyModel
                {
                    Id = x.Id,
                    CurrencyValue = x.CurrencyCode
                })
                .ToArray();

            IEnumerable<TradeTypeModel> tradeTypes = this.Context.TradeTypes
                .Select(x => new TradeTypeModel
                {
                    Id = x.Id,
                    TypeOfTrade = x.TypeOfTransaction
                })
                .ToList();

            IEnumerable<FutureModel> futureModels = this.Context.Features
                .Select(f => new FutureModel
                {
                    Id = f.Id,
                    FutureDescription = f.FutureDescription
                })
                .ToList();

            CreateEstateDropDownModel dropDownElements = new CreateEstateDropDownModel
            {
                Areas = areaModels,
                EstateTypeModels = estateTypeModels,
                CurrencyModels = currencyModels,
                TradeTypeModels = tradeTypes,
                FutureModels = futureModels
            };

            return dropDownElements;
        }

        public string CreateEstate(EstateModel model)
        {
            Estate estate = new Estate
            {
                Squaring = model.Squaring,
                CreatedOn = DateTime.UtcNow,
                EditedOn = null,
                IsArchived = false,
                ArchivedOn = null,
                ReportedOn = null,
                IsBanned = false,
                BannedOn = null,
                Floor = model.Floor,
                Price = model.Price,
                Description = model.Description,
                EstateType = Context.EstateTypes.FirstOrDefault(x => x.Id == model.EstateTypeId),
                TradeType = Context.TradeTypes.FirstOrDefault(x => x.Id == model.TypeOfTradeId),
                Area = Context.Areas.FirstOrDefault(x => x.Id == model.AreaId),
                City = Context.Cities.FirstOrDefault(x => x.Id == model.CityId),
                Neighborhood = Context.Neighborhoods.FirstOrDefault(x => x.Id == model.NeighborhoodId),
                Currency = Context.Currencies.FirstOrDefault(x => x.Id == model.CurrencyId)

            };

            foreach (var selectedFuture in model.SelectedFutures)
            {
                Feature feature = Context.Features.FirstOrDefault(x => x.Id == selectedFuture.Id);

                if (feature != null)
                {
                    estate.Features.Add(feature);
                }
            }

            this.Context.Estates.Add(estate);

            this.Context.SaveChanges();

            return estate.Id;
        }

        public async Task<IEnumerable<CityModel>> GetCitiesByAreaIdAsync(int id)
        {
            return await this.Context
                .Cities
                .Where(x => x.AreaId == id)
                .Select(c => new CityModel
                {
                    Id = c.Id,
                    CityName = c.CityName
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<NeighborhoodModel>> GetNeighborhoodsByCityIdAsync(int id)
        {
            return await this.Context
                 .Neighborhoods
                 .Where(c => c.City.Id == id)
                 .Select(n => new NeighborhoodModel
                 {
                     Id = n.Id,
                     Neighborhood = n.Name
                 })
                 .ToArrayAsync();
        }

        public async Task<IEnumerable<EstateListingViewModel>> GetAllEstatesAsync(int currentPage, int estatesPerPage)
        {
            if (!this.Context.Estates.Any())
            {
                return new EstateListingViewModel[0];
            }

            return await this.Context.Estates
                .Skip((currentPage - 1) * estatesPerPage)
                .Take(estatesPerPage)
                .Select(x => new EstateListingViewModel
                {
                    Id = x.Id,
                    Image = "Implement insert estate image!",
                    Title = x.TradeType.TypeOfTransaction,
                    Description = x.Description.Length < 30 ? x.Description : x.Description.Substring(0, 30) + "..."
                })
                .ToArrayAsync();
        }

        public async Task<int> GetCountOfAllEstatesAsync()
        {
            return await this.Context.Estates.CountAsync();
        }
    }
}
