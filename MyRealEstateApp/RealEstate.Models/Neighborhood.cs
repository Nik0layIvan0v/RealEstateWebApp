namespace RealEstate.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.DataBaseAttributesConstants;

    public class Neighborhood : BaseEntity
    {
        [Required]
        [MaxLength(MaxNeighborhoodNameLength)]
        public string Name { get; set; }

        [Required]
        public string CityId { get; set; }

        public City City { get; set; }
    }
}