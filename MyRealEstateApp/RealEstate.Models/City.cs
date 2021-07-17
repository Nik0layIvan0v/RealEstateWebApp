namespace RealEstate.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataBaseAttributesConstants;

    public class City
    {
        public City()
        {
            this.Neighborhoods = new HashSet<Neighborhood>();
            this.Estates = new HashSet<Estate>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(MaxCityNameLength)]
        public string CityName { get; set; }

        public int AreaId { get; set; }

        public Area Area { get; set; }

        public virtual ICollection<Neighborhood> Neighborhoods { get; set; }

        public virtual ICollection<Estate> Estates { get; set; }
    }
}