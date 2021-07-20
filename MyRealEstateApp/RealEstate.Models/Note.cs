namespace RealEstate.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Common.GlobalConstants;

    /// <summary>
    /// This entity is when user want to write some notes for property
    /// </summary>
    public class Note
    {
        [Required]
        [MaxLength(MaxNoteLength)]
        public string Content { get; set; }
        
        public DateTime DateOfCreation { get; set; }

        [Required]
        public string EstateId { get; set; }

        public Estate Estate { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}