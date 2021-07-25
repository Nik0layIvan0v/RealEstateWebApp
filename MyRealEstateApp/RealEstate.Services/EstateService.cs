using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RealEstate.Common.GlobalConstants;

namespace RealEstate.Services
{
    public class EstateService : IEstateService
    {
        private readonly RealEstateDbContext Context;

        public EstateService(RealEstateDbContext context)
        {
            this.Context = context;
        }

        public async Task<CreateEstateDropDownModel> GetDropDownDataAsync()
        {
          IEnumerable<AreaModel> areaModels =  await this.Context.Areas
                .Select(x => new AreaModel
                {
                    Id = x.Id,
                    Area = x.AreaName
                })
                .ToArrayAsync();

            IEnumerable<EstateTypeModel> estateTypeModels = await this.Context.EstateTypes
                .Select(x => new EstateTypeModel
                {
                    Id = x.Id,
                    Type = x.TypeOfProperty
                })
                .ToArrayAsync();

            IEnumerable<CurrencyModel> currencyModels = await this.Context.Currencies
                .Select(x => new CurrencyModel
                {
                    Id = x.Id,
                    CurrencyValue = x.CurrencyCode
                })
                .ToArrayAsync();

            IEnumerable<TradeTypeModel> tradeTypes = await this.Context.TradeTypes
                .Select(x => new TradeTypeModel
                {
                    Id = x.Id,
                    TypeOfTrade = x.TypeOfTransaction
                })
                .ToArrayAsync();

            IEnumerable<FutureModel> futureModels = await this.Context.Features
                .Select(f => new FutureModel
                {
                    Id = f.Id,
                    FutureDescription = f.FutureDescription
                })
                .ToArrayAsync();

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

        public async Task<string> CreateEstateAsync(EstateModel model)
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
                EstateType = await Context.EstateTypes.FirstOrDefaultAsync(x => x.Id == model.EstateTypeId),
                TradeType = await Context.TradeTypes.FirstOrDefaultAsync(x => x.Id == model.TypeOfTradeId),
                Area = await Context.Areas.FirstOrDefaultAsync(x => x.Id == model.AreaId),
                City = await Context.Cities.FirstOrDefaultAsync(x => x.Id == model.CityId),
                Neighborhood = await Context.Neighborhoods.FirstOrDefaultAsync(x => x.Id == model.NeighborhoodId),
                Currency = await Context.Currencies.FirstOrDefaultAsync(x => x.Id == model.CurrencyId),
                BrokerId = model.BrokerId
            };

            if (!model.Images.Any())
            {
                estate.Images.Add(new Image
                {
                    ImageContentBytes = DefaultEstateImage,
                    EstateId = estate.Id,
                });
            }
            else
            {
                foreach (var image in model.Images)
                {
                    estate.Images.Add(new Image
                    {
                        ImageContentBytes = image,
                        EstateId = estate.Id,
                    });
                }
            }

            foreach (var selectedFeature in model.SelectedFutures)
            {
                var estateFeature = new EstateFeature
                {
                    EstateId = estate.Id,
                    FeatureId = selectedFeature.Id,
                };

                estate.Features.Add(estateFeature);
            }

            await this.Context.Estates.AddAsync(estate);

            await this.Context.SaveChangesAsync();

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
                    Image = x.Images.FirstOrDefault(),
                    Title = x.TradeType.TypeOfTransaction,
                    Description = x.Description.Length < 30 ? x.Description : x.Description.Substring(0, 30) + "..."
                })
                .ToArrayAsync();
        }

        public async Task<int> GetCountOfAllEstatesAsync()
        {
            return await this.Context.Estates.CountAsync();
        }

        public async Task<EstateDetailsModel> GetEstateDetailsAsync(string id)
        {
            return await this.Context.Estates
                .Where(x => x.Id == id)
                .Select(x => new EstateDetailsModel
                {
                    Id = x.Id,
                    Squaring = x.Squaring,
                    CreatedOn = x.CreatedOn,
                    Floor = x.Floor,
                    Price = x.Price,
                    CurrencyCode = x.Currency.CurrencyCode,
                    EstateType = x.EstateType.TypeOfProperty,
                    TradeType = x.TradeType.TypeOfTransaction,
                    Area = x.Area.AreaName,
                    City = x.City.CityName,
                    Neighborhood = x.Neighborhood.Name,
                    Description = x.Description,
                    FutureModels = x.Features.Where(ef => ef.EstateId == x.Id).Select(f => f.Feature.FutureDescription).ToList(),
                    ImageFiles = x.Images
                })
                .FirstOrDefaultAsync();
        }
    }
}
