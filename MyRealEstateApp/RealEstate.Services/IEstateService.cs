using System.Collections.Generic;

namespace RealEstate.Services
{
    public interface IEstateService
    {
        IEnumerable<string> GetAllEstates();

        IEnumerable<string> GetByCategory();

        IEnumerable<string> GetByUserIdAsync();

        IEnumerable<string> GetBySearchAsync();

        IEnumerable<string> GetByCategoryIdAsync();

        void CreateEstate();

        void EstateDetails(string id); //<= return model

        void DeleteEstate(string id);

        void EditEstate(string ids);

        void ArchiveEstate(string id);

    }
}