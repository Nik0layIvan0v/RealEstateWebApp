namespace RealEstate.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.DataBaseAttributesConstants;

    public class Feature : BaseEntity
    {
        [Required]
        [MaxLength(MaxFutureDescriptionLength)]
        public string FutureDescription { get; set; }

        public string EstateId { get; set; }

        public Estate Estate { get; set; }
    }
}