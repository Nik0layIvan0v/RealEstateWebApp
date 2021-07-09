namespace RealEstate.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image : BaseEntity
    {
        public byte[] ImageContentBytes { get; set; }

        [Required]
        public string EstateId { get; set; }

        public Estate Estate { get; set; }
    }
}