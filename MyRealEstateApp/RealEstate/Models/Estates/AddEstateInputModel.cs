﻿using RealEstate.Services.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Models.Estates
{
    using static Common.DataBaseAttributesConstants;

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

        public CurrencyModel Currency { get; set; }

        [Display(Name = "Estate Type:")]
        public string EstateTypeId { get; set; }

        [Display(Name = "Area:")]
        public int AreaId { get; set; }


        [Display(Name = "CityName:")]
        public int CityId { get; set; }

        [Display(Name = "Neighborhood:")]
        public int NeighborhoodId { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Type Of trade:")]
        public string TypeOfTradeId { get; set; }

        //public List<FutureModel> FuturesId { get; set; }

        public IEnumerable<EstateTypeModel> EstateTypeViewModels { get; set; }

        public IEnumerable<TradeTypeModel> TypeOfDeals { get; set; }

        public IEnumerable<CurrencyModel> CurrencyViewModels { get; set; }

        public IEnumerable<AreaModel> AreasViewModels { get; set; }

        public List<FutureModel> FutureModels { get; set; }
    }
}
