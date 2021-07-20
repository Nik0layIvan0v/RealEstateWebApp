namespace RealEstate.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.GlobalConstants;

    public class Neighborhood
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNeighborhoodNameLength)]
        public string Name { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }
    }
}