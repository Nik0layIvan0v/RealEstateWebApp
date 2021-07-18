using System.ComponentModel.DataAnnotations;

namespace RealEstate.Services.Models
{
    public class TradeTypeModel
    {
        [Required]
        public string Id { get; set; }

        public string TypeOfTrade { get; set; }
    }
}