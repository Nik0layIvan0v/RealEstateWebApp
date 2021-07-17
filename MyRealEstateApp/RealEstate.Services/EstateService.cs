using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
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

        public CreateEstateDropDownViewModel GetDropDownData()
        {
            ICollection<AreaViewModel> areasViewModels = this.Context.Areas
                .Select(x => new AreaViewModel
                {
                    Id = x.Id,
                    Area = x.AreaName
                })
                .ToArray();

            ICollection<EstateTypeViewModel> estateTypeViewModels = this.Context.EstateTypes
                .Select(x => new EstateTypeViewModel
                {
                    Id = x.Id,
                    Type = x.TypeOfProperty
                })
                .ToArray();

            IEnumerable<CurrencyViewModel> currencyViewModels = this.Context.Currencies
                .Select(x => new CurrencyViewModel
                {
                    Id = x.Id,
                    CurrencyValue = x.CurrencyCode
                })
                .ToArray();

            CreateEstateDropDownViewModel dropDownElements = new CreateEstateDropDownViewModel
            {
                Areas = areasViewModels,
                EstateTypeViewModels = estateTypeViewModels,
                CurrencyViewModels = currencyViewModels,
            };

            return dropDownElements;
        }

        public IEnumerable<string> GetAllEstates()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetByCategory()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetByUserIdAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetBySearchAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetByCategoryIdAsync()
        {
            throw new NotImplementedException();
        }

        public void CreateEstate()
        {
        }

        public async Task<IEnumerable<CityViewModel>> GetCitiesByAreaIdAsync(int id)
        {
            return await this.Context
                .Cities
                .Where(x => x.AreaId == id)
                .Select(c => new CityViewModel
                {
                    Id = c.Id,
                    CityName = c.CityName
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<NeighborhoodViewModel>> GetNeighborhoodsByCityIdAsync(int id)
        {
           return await this.Context
                .Neighborhoods
                .Where(c => c.City.Id == id )
                .Select(n => new NeighborhoodViewModel
                {
                    Id = n.Id,
                    Neighborhood = n.Name
                })
                .ToArrayAsync();
        }

        public void EstateDetails(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteEstate(string id)
        {
            throw new NotImplementedException();
        }

        public void EditEstate(string ids)
        {
            throw new NotImplementedException();
        }

        public void ArchiveEstate(string id)
        {
            throw new NotImplementedException();
        }

        public void GetPropertyTypes()
        {
        }

        public void GetCurrenciesTypes()
        {
        }

    }
}
