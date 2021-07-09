namespace RealEstate.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataBaseAttributesConstants;

    public class City : BaseEntity
    {
        public City()
        {
            this.Neighborhoods = new HashSet<Neighborhood>();
            this.Estates = new HashSet<Estate>();
        }

        [Required]
        [MaxLength(MaxCityNameLength)]
        public string CityName { get; set; }

        [Required]
        public string AreaId { get; set; }

        public Area Area { get; set; }

        public virtual ICollection<Neighborhood> Neighborhoods { get; set; }

        public virtual ICollection<Estate> Estates { get; set; }
    }
}