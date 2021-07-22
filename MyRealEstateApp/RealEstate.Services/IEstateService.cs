using RealEstate.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface IEstateService
    {
        CreateEstateDropDownModel GetDropDownData();

        Task<string> CreateEstate(EstateModel model);

        Task<IEnumerable<CityModel>> GetCitiesByAreaIdAsync(int id);

        Task<IEnumerable<NeighborhoodModel>> GetNeighborhoodsByCityIdAsync(int id);

        Task<IEnumerable<EstateListingViewModel>> GetAllEstatesAsync(int currentPage, int estatesPerPage);

        Task<int> GetCountOfAllEstatesAsync();

        Task<EstateDetailsModel> GetEstateDetailsAsync(string id);

        Task<IEnumerable<LastAddedEstateModel>> GetLastAddedEstates(int count);

        Task<bool> IsUserIsBrokerAsync(string id);


        //IEnumerable<string> GetByCategory();

        //IEnumerable<string> GetByUserIdAsync();

        //IEnumerable<string> GetBySearchAsync();


        //void DeleteEstate(string id);

        //void EditEstate(string ids);

        //void ArchiveEstate(string id);
    }
}