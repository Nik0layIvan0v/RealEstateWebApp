using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static RealEstate.Common.DataBaseAttributesConstants;

namespace RealEstate.Models.Estates
{
    public class CreateEstateViewModel
    {
        public decimal Squaring { get; set; }

        public int Floor { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public string CurrencyId { get; set; }

        public List<CurrencyViewModel> Currency { get; set; }

        public string EstateTypeId { get; set; }

        public List<EstateTypeViewModel> EstateTypes { get; set; }

        public string AreaId { get; set; }

        public List<AreaViewModel> Areas { get; set; }

        public string CityId { get; set; }

        public List<CityViewModel> Cities { get; set; }

        public string NeighborhoodId { get; set; }

        public List<NeighborhoodViewModel> Neighborhoods { get; set; }
    }
}