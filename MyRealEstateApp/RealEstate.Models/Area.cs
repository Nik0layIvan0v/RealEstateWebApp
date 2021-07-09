namespace RealEstate.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataBaseAttributesConstants;

    public class Area : BaseEntity
    {
        public Area()
        {
            this.Estates = new HashSet<Estate>();
            this.Cities = new HashSet<City>();
        }

        [Required]
        [MaxLength(AreaMaxNameLength)]
        public string AreaName { get; set; }

        public virtual ICollection<City> Cities { get; set; }

        public virtual ICollection<Estate> Estates { get; set; }
    }
}
