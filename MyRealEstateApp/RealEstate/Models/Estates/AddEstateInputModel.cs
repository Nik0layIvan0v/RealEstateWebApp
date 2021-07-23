using RealEstate.Services.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RealEstate.Models.Estates
{
    using static Common.GlobalConstants;

    public class AddEstateInputModel
    {
        [Range(1, 9999.999, ErrorMessage = "Squaring must be between 1 and 9999.999")]
        [Display(Name = "Squaring:")]
        public decimal Squaring { get; set; }

        [Range(1, 200, ErrorMessage = "Floor cannot be less than 1 and more than 200")]
        [Display(Name = "Floor:")]
        public int Floor { get; set; }

        [Range(1, 99999999, ErrorMessage = "Price cannot be negative number or more than 999999999")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Currency cannot be empty")]
        [Display(Name = "Currency:")]
        public string CurrencyId { get; set; }

        public CurrencyModel Currency { get; set; }

        [Display(Name = "Estate Type:")]
        [Required(ErrorMessage = "Estate Type cannot be empty")]
        public string EstateTypeId { get; set; }

        [Display(Name = "Area:")]
        [Required(ErrorMessage = "Area cannot be empty")]
        public int AreaId { get; set; }

        [Display(Name = "City Name:")]
        [Required(ErrorMessage = "City Name cannot be empty")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Neighborhood cannot be empty")]
        [Display(Name = "Neighborhood:")]
        public int NeighborhoodId { get; set; }

        [Required(ErrorMessage = "Description cannot be empty")]
        [StringLength(MaxDescriptionLength, MinimumLength = 5, ErrorMessage = "Description must be with minimum 5 characters long")]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Type Of trade cannot be empty")]
        [Display(Name = "Type Of trade:")]
        public string TypeOfTradeId { get; set; }

        public IEnumerable<EstateTypeModel> EstateTypeViewModels { get; set; }

        public IEnumerable<TradeTypeModel> TypeOfDeals { get; set; }

        public IEnumerable<CurrencyModel> CurrencyViewModels { get; set; }

        public IEnumerable<AreaModel> AreasViewModels { get; set; }

        public List<FutureModel> FutureModels { get; set; }

        public List<IFormFile> ImageFiles { get; set; }
    }
}
