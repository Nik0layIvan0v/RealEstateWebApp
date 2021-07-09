namespace RealEstate.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataBaseAttributesConstants;

    public class Currency : BaseEntity
    {
        public Currency()
            => this.Estates = new HashSet<Estate>();

        [Required]
        [MaxLength(MaxCurrencyCodeLength)]
        public string CurrencyCode { get; set; }

        public virtual ICollection<Estate> Estates { get; set; }
    }
}