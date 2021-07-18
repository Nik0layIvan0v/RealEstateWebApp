using System.ComponentModel.DataAnnotations;

namespace RealEstate.Services.Models
{
    public class NeighborhoodModel
    {
        [Required]
        public int Id { get; set; }

        public string Neighborhood { get; set; }
    }
}