using System.Collections.Generic;

namespace RealEstate.Services.Models
{
    public class CreateEstateDropDownViewModel
    {
        public IEnumerable<EstateTypeViewModel> EstateTypeViewModels { get; set; }

        public IEnumerable<CurrencyViewModel> CurrencyViewModels { get; set; }

        public IEnumerable<AreaViewModel> Areas { get; set; }

    }
}