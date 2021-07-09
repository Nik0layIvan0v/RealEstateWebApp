namespace RealEstate.Models
{
    public class UserEstateWishList : BaseEntity
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string EstateId { get; set; }

        public Estate Estate { get; set; }
    }
}