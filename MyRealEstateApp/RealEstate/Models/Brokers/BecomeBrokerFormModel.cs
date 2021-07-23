using System.ComponentModel.DataAnnotations;
using static RealEstate.Common.GlobalConstants;

namespace RealEstate.Models.Brokers
{
    public class BecomeBrokerFormModel
    {
        [Required]
        [Display(Name = "Broker Name: ")]
        [StringLength(MaxBrokerNameLength, MinimumLength = MinBrokerNameLength)]
        public string BrokerName { get; set; }


        [Required]
        [Display(Name = "Broker Phone Number: ")]
        [StringLength(MaxBrokerPhoneNumberLength, MinimumLength = MinBrokerPhoneNumberLength)]
        [RegularExpression(BrokerPhoneNumberRegularExpression)]
        public string PhoneNumber { get; set; }
    }
}