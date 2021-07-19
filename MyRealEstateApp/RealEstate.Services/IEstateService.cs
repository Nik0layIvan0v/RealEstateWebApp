using RealEstate.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface IEstateService
    {
        CreateEstateDropDownModel GetDropDownData();

        string CreateEstate(EstateModel model);

        Task<IEnumerable<CityModel>> GetCitiesByAreaIdAsync(int id);

        Task<IEnumerable<NeighborhoodModel>> GetNeighborhoodsByCityIdAsync(int id);

        Task<IEnumerable<EstateListingViewModel>> GetAllEstatesAsync(int currentPage, int estatesPerPage);

        Task<int> GetCountOfAllEstatesAsync();

        //IEnumerable<string> GetByCategory();

        //IEnumerable<string> GetByUserIdAsync();

        //IEnumerable<string> GetBySearchAsync();

        //void EstateDetails(string id); //<= return model

        //void DeleteEstate(string id);

        //void EditEstate(string ids);

        //void ArchiveEstate(string id);
    }
}