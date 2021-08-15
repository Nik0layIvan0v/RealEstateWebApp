using System.Collections.Generic;
using RealEstate.Services.Models;

namespace RealEstate.Models.Estates
{
    public class AllEstateQueryModel
    {
        public IEnumerable<string> DealTypes { get; set; }

        public string SearchTerm { get; set; }

        public EstateSorting EstateSorting { get; set; }

        public int EstatesPerPage { get; set; } = 6;

        public int CurrentPage { get; set; } = 1;

        public int TotalEstates { get; set; }

        public IEnumerable<EstateListingViewModel> EstateListingViewModels { get; set; }
    }
}