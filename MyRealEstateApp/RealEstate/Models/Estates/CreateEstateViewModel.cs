using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RealEstate.Services.Models;
using static RealEstate.Common.DataBaseAttributesConstants;

namespace RealEstate.Models.Estates
{
    public class CreateEstateViewModel
    {
        [Range(0, 9999.999)]
        [Display(Name = "Squaring:")]
        public decimal Squaring { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Floor:")]
        public int Floor { get; set; }

        [Range(0,99999.999)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Currency:")]
        public string CurrencyId { get; set; }

        public IEnumerable<CurrencyViewModel> Currency { get; set; }

        [Display(Name = "Estate Type:")]
        public string EstateTypeId { get; set; }

        public IEnumerable<EstateTypeViewModel> EstateTypes { get; set; }

        [Required]
        [Display(Name = "Area:")]
        public string AreaId { get; set; }

        public IEnumerable<AreaViewModel> Areas { get; set; }

        [Required]
        [Display(Name = "CityName:")]
        public string CityId { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }

        [Required]
        [Display(Name = "Neighborhood:")]
        public string NeighborhoodId { get; set; }

        public IEnumerable<NeighborhoodViewModel> Neighborhoods { get; set; }
    }
}