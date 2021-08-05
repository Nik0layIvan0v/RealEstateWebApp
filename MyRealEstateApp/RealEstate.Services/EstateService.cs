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
        private readonly IHomeService HomeService;

        public EstateService(RealEstateDbContext context, IHomeService homeService)
        {
            this.Context = context;
            this.HomeService = homeService;
        }

        public async Task<CreateEstateDropDownModel> GetDropDownDataAsync()
        {
            IEnumerable<AreaModel> areaModels = await this.Context.Areas
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

        public async Task<string> CreateEstateAsync(EstateServiceModel model)
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
                    ImageContentBytes = await this.HomeService.GetCompanyLogoAsync(),
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
                    ImageFiles = x.Images,
                    Comments = new List<CommentServiceModel>()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> EditEstateAsync(string estateId, EstateServiceModel formModel)
        {
            Estate estate = await this.Context.Estates.FirstOrDefaultAsync(x => x.Id == estateId);

            if (estate == null)
            {
                return false;
            }

            if (estate.BrokerId != formModel.BrokerId)
            {
                return false;
            }

            if (formModel.Images.Count != 0)
            {
                var estateImages = await this.Context.Images.Where(image => image.EstateId == estateId).ToListAsync();

                this.Context.Images.RemoveRange(estateImages);

                foreach (var formImage in formModel.Images)
                {
                    Image image = new Image
                    {
                        EstateId = estateId,
                        ImageContentBytes = formImage
                    };

                    estate.Images.Add(image);
                }
            }

            if (formModel.SelectedFutures != null && formModel.SelectedFutures.Count != 0)
            {
                var features = this.Context.EstateFeatures.Where(ef => ef.EstateId == estateId).ToList();

                this.Context.EstateFeatures.RemoveRange(features);

                await this.Context.SaveChangesAsync();

                estate.Features = formModel.SelectedFutures.Select(x => new EstateFeature
                {
                    EstateId = estateId,
                    FeatureId = x.Id,
                }).ToList();
            }

            estate.Squaring = formModel.Squaring;
            estate.EditedOn = DateTime.UtcNow;
            estate.Floor = formModel.Floor;
            estate.Price = formModel.Price;
            estate.Description = formModel.Description;
            estate.CurrencyId = formModel.CurrencyId;
            estate.EstateTypeId = formModel.EstateTypeId;
            estate.TradeTypeId = formModel.TypeOfTradeId;
            estate.AreaId = formModel.AreaId;
            estate.CityId = formModel.CityId;
            estate.NeighborhoodId = formModel.NeighborhoodId;

            await this.Context.SaveChangesAsync();

            return true;
        }

        public async Task<EstateServiceModel> GetEstateFormModelById(string id)
        {
            return await this.Context.Estates
                .Where(x => x.Id == id)
                .Select(x => new EstateServiceModel
                {
                    BrokerId = x.BrokerId,
                    Squaring = x.Squaring,
                    Floor = x.Floor,
                    Price = x.Price,
                    CurrencyId = x.CurrencyId,
                    EstateTypeId = x.EstateTypeId,
                    AreaId = x.AreaId,
                    CityId = x.CityId,
                    NeighborhoodId = x.NeighborhoodId,
                    Description = x.Description,
                    TypeOfTradeId = x.EstateTypeId,
                    SelectedFutures = x.Features.Select(x => new FutureModel
                    {
                        Id = x.FeatureId,
                        FutureDescription = x.Feature.FutureDescription,
                        IsChecked = true

                    }).ToList(),
                    Images = x.Images.Select(image => image.ImageContentBytes).ToList(),
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetEstateBrokerId(string estateId)
        {
            return await this.Context.Estates.Where(x => x.Id == estateId).Select(x => x.BrokerId).FirstOrDefaultAsync();
        }
    }
}
