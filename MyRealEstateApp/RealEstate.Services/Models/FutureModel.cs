using System.ComponentModel.DataAnnotations;

namespace RealEstate.Services.Models
{
    public class FutureModel
    {
        [Required]
        public string Id { get; set; }

        public string FutureDescription { get; set; }

        public bool IsChecked { get; set; }
    }
}