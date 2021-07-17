using RealEstate.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface IEstateService
    {
        CreateEstateDropDownViewModel GetDropDownData();

        //IEnumerable<string> GetAllEstates();

        //IEnumerable<string> GetByCategory();

        //IEnumerable<string> GetByUserIdAsync();

        //IEnumerable<string> GetBySearchAsync();

        void CreateEstate();

        //void EstateDetails(string id); //<= return model

        //void DeleteEstate(string id);

        //void EditEstate(string ids);

        //void ArchiveEstate(string id);
        Task<IEnumerable<CityViewModel>> GetCitiesByAreaIdAsync(int id);

        Task<IEnumerable<NeighborhoodViewModel>> GetNeighborhoodsByCityIdAsync(int id);
    }
}