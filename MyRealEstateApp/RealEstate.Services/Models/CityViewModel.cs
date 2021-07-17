using System.ComponentModel.DataAnnotations;

namespace RealEstate.Services.Models
{
    public class CityViewModel
    {
        public int Id { get; set; }

        [Required]
        public string CityName { get; set; }
    }
}
