namespace RealEstate.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity
    {
        public BaseEntity()
            => this.Id = Guid.NewGuid().ToString();

        [Key]
        [Required]
        public string Id { get; set; }
    }
}