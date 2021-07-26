namespace RealEstate.Models.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using static Common.GlobalConstants;

    public class CommentFormModel
    {
        [Required]
        public string estateId { get; set; }

        [Required]
        [StringLength(MaxCommentLength, MinimumLength = MinCommentLength)]
        public string Content { get; set; }
    }
}
