namespace RealEstate.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using static Common.GlobalConstants;

    public class EstateType : BaseEntity
    {
        public EstateType() 
            => this.Estates = new HashSet<Estate>();

        [Required]
        [MaxLength(MaxEstateTypeLength)]
        public string TypeOfProperty { get; set; }

        public virtual ICollection<Estate> Estates { get; set; }
    }
}