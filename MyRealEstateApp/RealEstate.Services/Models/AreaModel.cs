using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Services.Models
{
    public class AreaModel
    {
        public int Id { get; set; }

        [Required]
        public string Area { get; set; }

    }
}
