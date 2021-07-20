using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static RealEstate.Common.GlobalConstants;

namespace RealEstate.Services.Models
{
    public class EstateDetailsModel
    {
        [Required]
        public string Id { get; set; }

        [Range(1, 9999.999, ErrorMessage = "Squaring must be between 1 and 9999.999")]
        [Display(Name = "Squaring:")]
        public decimal Squaring { get; set; }

        [Display(Name = "Created on:")]
        public DateTime CreatedOn { get; set; }

        //public bool IsArchived { get; set; }

        //public DateTime? ArchivedOn { get; set; }

        //public DateTime? ReportedOn { get; set; }

        [Range(1, 200, ErrorMessage = "Floor cannot be less than 1 and more than 200")]
        [Display(Name = "Floor:")]
        public int Floor { get; set; }

        [Display(Name = "Price:")]
        [Range(1, 99999.999, ErrorMessage = "Price cannot be negative number or more than 999999999.999")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Currency Code:")]
        [StringLength(MaxCurrencyCodeLength, MinimumLength = MinCurrencyCodeLength)]
        public string CurrencyCode { get; set; }

        [Display(Name = "Estate Type:")]
        [Required(ErrorMessage = "Estate Type cannot be empty")]
        [StringLength(MaxEstateTypeLength, MinimumLength = MinEstateTypeLength)]
        public string EstateType { get; set; }

        [Required]
        [Display(Name = "Type of trade: ")]
        [StringLength(MaxTradeTypeLength, MinimumLength = MinTradeTypeLength)]
        public string TradeType { get; set; }

        [Display(Name = "Area:")]
        [Required(ErrorMessage = "Area cannot be empty")]
        [StringLength(MaxAreaNameLength, MinimumLength = MinAreaNameLegth)]
        public string Area { get; set; }

        [Display(Name = "City Name:")]
        [Required(ErrorMessage = "City Name cannot be empty")]
        [StringLength(MaxCityNameLength, MinimumLength = MinCityNameLength)]
        public string City { get; set; }

        [Required(ErrorMessage = "Neighborhood cannot be empty")]
        [Display(Name = "Neighborhood:")]
        public string Neighborhood { get; set; }


        [Required(ErrorMessage = "Description cannot be empty")]
        [Display(Name = "Description:")]
        [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength, ErrorMessage = "Description must be with minimum 5 characters long")]
        public string Description { get; set; }

        public ICollection<string> FutureModels { get; set; }

        public ICollection<Image> ImageFiles { get; set; }
    }
}