﻿using RealEstate.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface IEstateService
    {
        Task<CreateEstateDropDownModel> GetDropDownDataAsync();

        Task<string> CreateEstateAsync(EstateModel model);

        Task<IEnumerable<CityModel>> GetCitiesByAreaIdAsync(int id);

        Task<IEnumerable<NeighborhoodModel>> GetNeighborhoodsByCityIdAsync(int id);

        Task<IEnumerable<EstateListingViewModel>> GetAllEstatesAsync(int currentPage, int estatesPerPage);

        Task<int> GetCountOfAllEstatesAsync();

        Task<EstateDetailsModel> GetEstateDetailsAsync(string id);

        Task<bool> IsUserIsBrokerAsync(string id);

        Task<int> GetBrokerIdAsync(string loggedUserId);

        //IEnumerable<string> GetByCategory();

        //IEnumerable<string> GetBySearchAsync();

        //void DeleteEstate(string id);

        //void EditEstate(string ids);

        //void ArchiveEstate(string id);
    }
}