namespace RealEstate.Models.Comments
{
    using System.ComponentModel.DataAnnotations;
    using static Common.GlobalConstants;

    public class CommentFormModel
    {
        [Required]
        public string EstateId { get; set; }

        [Required]
        [StringLength(MaxCommentLength, MinimumLength = MinCommentLength)]
        public string Content { get; set; }
    }
}
