using System.Collections.Generic;

namespace RealEstate.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.GlobalConstants;

    public class Feature : BaseEntity
    {
        public Feature()
        {
            this.Estates = new HashSet<EstateFeature>();
        }

        [Required]
        [MaxLength(MaxFutureDescriptionLength)]
        public string FutureDescription { get; set; }

        public virtual ICollection<EstateFeature> Estates { get; set; }
    }
}