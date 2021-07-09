using System.Collections.Generic;

namespace RealEstate.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TradeType : BaseEntity
    {
        public TradeType()
            => this.Estates = new HashSet<Estate>();

        [Required]
        public string TypeOfTransaction { get; set; }

        public ICollection<Estate> Estates { get; set; }
    }
}   