using RealEstate.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface IEstateService
    {
        Task<CreateEstateDropDownModel> GetDropDownDataAsync();

        Task<string> CreateEstateAsync(EstateServiceModel model);

        Task<bool> EditEstateAsync(string estateId, EstateServiceModel model);

        Task<IEnumerable<CityModel>> GetCitiesByAreaIdAsync(int id);

        Task<IEnumerable<NeighborhoodModel>> GetNeighborhoodsByCityIdAsync(int id);

        Task<IEnumerable<EstateListingViewModel>> GetAllEstatesAsync(int currentPage, int estatesPerPage);

        Task<int> GetCountOfAllEstatesAsync();

        Task<EstateDetailsModel> GetEstateDetailsAsync(string id);

        Task<EstateServiceModel> GetEstateFormModelById(string id);

        Task<int> GetEstateBrokerId(string estateId);

        Task<bool> DeleteEstateAsync(string id);

        //IEnumerable<string> GetByCategory();

        //IEnumerable<string> GetBySearchAsync();

        //void ArchiveEstate(string id);
    }
}