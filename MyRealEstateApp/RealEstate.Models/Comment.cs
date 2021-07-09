using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        public string EstateId { get; set; }

        public Estate Estate { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}