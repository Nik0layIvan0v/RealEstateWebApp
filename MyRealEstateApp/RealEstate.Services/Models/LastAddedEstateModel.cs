using RealEstate.Models;

namespace RealEstate.Services.Models
{
    public class LastAddedEstateModel
    {
        public string Id { get; set; }

        public decimal Squaring { get; set; }

        public int Floor { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; }

        public string TradeType { get; set; }

        public string Location { get; set; }
    }
}