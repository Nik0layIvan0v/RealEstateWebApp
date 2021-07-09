using System.Collections.Generic;

namespace RealEstate.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Notes = new HashSet<Note>();
            this.UserEstateWish = new HashSet<UserEstateWishList>();
        }   

        public virtual ICollection<UserEstateWishList> UserEstateWish { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}