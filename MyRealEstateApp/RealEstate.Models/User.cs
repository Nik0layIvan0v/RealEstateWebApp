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
            this.Followers = new HashSet<FollowerFollowing>();
            this.Followings = new HashSet<FollowerFollowing>();
            this.ReportedUsers = new HashSet<Report>();
            this.ReportingUsers = new HashSet<Report>();
            this.Comments = new HashSet<Comment>();
        }

        public virtual ICollection<Report> ReportedUsers { get; set; }

        public virtual ICollection<Report> ReportingUsers { get; set; }

        public virtual ICollection<UserEstateWishList> UserEstateWish { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        public virtual ICollection<FollowerFollowing> Followers { get; set; }

        public virtual ICollection<FollowerFollowing> Followings { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}