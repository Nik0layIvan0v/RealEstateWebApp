using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models.Estates
{
    using static Common.DataBaseAttributesConstants;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddEstateInputModel
    {
        [Range(0, 9999.999)]
        [Display(Name = "Squaring:")]
        public decimal Squaring { get; set; }

        [Range(0, 100)]
        [Display(Name = "Floor:")]
        public int Floor { get; set; }

        [Range(0, 99999.999)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Currency:")]
        public string CurrencyId { get; set; }

        [Display(Name = "Estate Type:")]
        public string EstateTypeId { get; set; }

        [Required]
        [Display(Name = "Area:")]
        public string AreaId { get; set; }

        [Required]
        [Display(Name = "CityName:")]
        public string CityId { get; set; }

        [Required]
        [Display(Name = "Neighborhood:")]
        public string NeighborhoodId { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        [Display(Name = "Description:")]
        public string Description { get; set; }
    }
}
