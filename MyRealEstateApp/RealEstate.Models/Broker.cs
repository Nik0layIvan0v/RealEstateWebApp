using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static RealEstate.Common.GlobalConstants;

namespace RealEstate.Models
{
    public class Broker
    {
        public Broker()
        {
            //this.Comments = new HashSet<Comment>();
            /*this.Followers = new HashSet<FollowerFollowing>();*/
            this.Estates = new HashSet<Estate>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxBrokerNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxBrokerPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        //public IdentityUser User { get; set; }

        public virtual ICollection<Estate> Estates { get; set; }

        //public virtual ICollection<Comment> Comments { get; set; }

        //public virtual ICollection<FollowerFollowing> Followers { get; set; }


    }
}