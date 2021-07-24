using System;

namespace RealEstate.Services.Models
{
    public class MyEstateServiceModel
    {
        public string Id { get; set; }

        public byte[] FirstImage { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Area { get; set; }

        public string City { get; set; }

        public decimal Price { get; set; }

        public string CurrencyCode { get; set; }

        public string EstateType { get; set; }

        public string TradeType { get; set; }
    }
}