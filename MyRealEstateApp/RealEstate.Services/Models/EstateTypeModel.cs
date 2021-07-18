using System.ComponentModel.DataAnnotations;

namespace RealEstate.Services.Models
{
    public class EstateTypeModel
    {
        [Required]
        public string Id { get; set; }

        public string Type { get; set; }
    }
}