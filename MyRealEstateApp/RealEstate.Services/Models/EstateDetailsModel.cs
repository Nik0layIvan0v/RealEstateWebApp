using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Services.Models
{
    public class EstateDetailsModel
    {
        [Required]
        public string Id { get; set; }

        [Display(Name = "Squaring:")]
        public decimal Squaring { get; set; }

        [Display(Name = "Created on:")]
        public DateTime CreatedOn { get; set; }

        //public bool IsArchived { get; set; }

        //public DateTime? ArchivedOn { get; set; }

        //public DateTime? ReportedOn { get; set; }

        [Display(Name = "Floor:")]
        public int Floor { get; set; }

        [Display(Name = "Price:")]
        public decimal Price { get; set; }

        [Display(Name = "Currency Code:")]
        public string CurrencyCode { get; set; }

        [Display(Name = "Estate Type:")]
        public string EstateType { get; set; }

        [Display(Name = "Type of trade: ")]
        public string TradeType { get; set; }

        [Display(Name = "Area:")]
        public string Area { get; set; }

        [Display(Name = "City Name:")]
        public string City { get; set; }

        [Display(Name = "Neighborhood:")]
        public string Neighborhood { get; set; }

        [Display(Name = "Description:")]
        public string Description { get; set; }

        public IEnumerable<string> FutureModels { get; set; }

        public IEnumerable<Image> ImageFiles { get; set; }
    }
}