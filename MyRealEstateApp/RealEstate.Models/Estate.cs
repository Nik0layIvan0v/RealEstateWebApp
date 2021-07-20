namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.GlobalConstants;

    public class Estate : BaseEntity
    {
        public Estate()
        {
            this.Notes = new HashSet<Note>();
            this.Features = new HashSet<Feature>();
            this.Images = new HashSet<Image>();
            this.Reports = new HashSet<Report>();
            this.UserEstateWishList = new HashSet<UserEstateWishList>();
            this.Comments = new HashSet<Comment>();
        }

        public decimal Squaring { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? EditedOn { get; set; }

        public bool IsArchived { get; set; }

        public DateTime? ArchivedOn { get; set; }

        public DateTime? ReportedOn { get; set; }

        public bool IsBanned { get; set; }

        public DateTime? BannedOn { get; set; }

        public int Floor { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public string CurrencyId { get; set; }

        public Currency Currency { get; set; }

        [Required]
        public string EstateTypeId { get; set; }

        public EstateType EstateType { get; set; }

        [Required]
        public string TradeTypeId { get; set; }

        public TradeType TradeType { get; set; }

        public int AreaId { get; set; }

        public Area Area { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public int NeighborhoodId { get; set; }

        public Neighborhood Neighborhood { get; set; }

        public virtual ICollection<UserEstateWishList> UserEstateWishList { get; set; }

        public virtual ICollection<Image> Images { get; set; }  

        public virtual ICollection<Note> Notes { get; set; }

        public virtual ICollection<Feature> Features { get; set; }

        public virtual ICollection<Report> Reports { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}