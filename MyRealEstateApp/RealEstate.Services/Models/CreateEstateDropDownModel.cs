using System.Collections.Generic;

namespace RealEstate.Services.Models
{
    public class CreateEstateDropDownModel
    {
        public IEnumerable<EstateTypeModel> EstateTypeModels { get; set; }

        public IEnumerable<CurrencyModel> CurrencyModels { get; set; }

        public IEnumerable<AreaModel> Areas { get; set; }

        public IEnumerable<TradeTypeModel> TradeTypeModels { get; set; }

        public IEnumerable<FutureModel> FutureModels { get; set; }

    }

}