using System.ComponentModel.DataAnnotations;

namespace RealEstate.Services.Models
{
    public class CurrencyModel
    {
        [Required]
        public string Id { get; set; }

        public string CurrencyValue { get; set; }
    }
}
