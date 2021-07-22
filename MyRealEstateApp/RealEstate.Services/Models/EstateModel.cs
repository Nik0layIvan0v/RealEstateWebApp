using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Services.Models
{
    public class EstateModel
    {
        public string BrokerId { get; set; }

        public decimal Squaring { get; set; }

        public int Floor { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string CurrencyId { get; set; }

        [Required]
        public string EstateTypeId { get; set; }

        public int AreaId { get; set; }

        public int CityId { get; set; }

        public int NeighborhoodId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string TypeOfTradeId { get; set; }   

        public List<FutureModel> SelectedFutures { get; set; }

        public List<byte[]> Images { get; set; }
    }
}