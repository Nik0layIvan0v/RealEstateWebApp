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

        public async Task<string> CreateEstate(EstateModel model)
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

            foreach (var selectedFuture in model.SelectedFutures)
            {
                Feature feature = Context.Features.FirstOrDefault(x => x.Id == selectedFuture.Id);

                if (feature != null)
                {
                    estate.Features.Add(feature);
                }
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
                    FutureModels = x.Features.Select(feature => feature.FutureDescription).ToList(),
                    ImageFiles = x.Images
                })
                .FirstOrDefaultAsync();
        }
    }
}
