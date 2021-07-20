namespace RealEstate.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Common.GlobalConstants;

    public class Report : BaseEntity
    {
        [Required]
        public string ReportingUserId { get; set; }

        public User ReportingUser { get; set; }

        [Required]
        public string ReportedUserId { get; set; }

        public User ReportedUser { get; set; }

        [Required]
        public string ReportedEstateId { get; set; }

        public Estate ReportedEstate { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public DateTime ReportedOn { get; set; }

        public bool IsArchived { get; set; }
    }
}