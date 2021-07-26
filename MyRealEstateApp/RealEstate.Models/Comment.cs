namespace RealEstate.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.GlobalConstants;

    public class Comment : BaseEntity
    {
        [Required]
        [MaxLength(MaxCommentLength)]
        public string CommentContent { get; set; }

        [Required]
        public string EstateId { get; set; }

        public Estate Estate { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}