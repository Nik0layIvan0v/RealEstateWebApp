using System.Collections.Generic;

namespace RealEstate.Services.Models
{
    public class EstateModel
    {
        public decimal Squaring { get; set; }

        public int Floor { get; set; }

        public decimal Price { get; set; }

        public string CurrencyId { get; set; }

        public string EstateTypeId { get; set; }

        public int AreaId { get; set; }

        public int CityId { get; set; }

        public int NeighborhoodId { get; set; }

        public string Description { get; set; }

        public string TypeOfTradeId { get; set; }   

        public List<FutureModel> SelectedFutures { get; set; }
    }
}