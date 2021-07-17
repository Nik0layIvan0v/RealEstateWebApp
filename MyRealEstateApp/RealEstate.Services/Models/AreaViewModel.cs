using System.ComponentModel.DataAnnotations;

namespace RealEstate.Services.Models
{
    public class AreaViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Area { get; set; }
    }
}
