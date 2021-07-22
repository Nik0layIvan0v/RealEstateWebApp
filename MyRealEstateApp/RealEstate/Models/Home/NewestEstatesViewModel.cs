using System.Collections.Generic;
using System.Linq;
using RealEstate.Services.Models;

namespace RealEstate.Models.Home
{
    public class NewestEstatesViewModel
    {
        public NewestEstatesViewModel()
        {
            this.LastAddedEstatesViewModels = new List<LastAddedEstateModel>();
        }

        public ICollection<LastAddedEstateModel> LastAddedEstatesViewModels { get; private set; }

        public void AddEstates(IEnumerable<LastAddedEstateModel> estates)
        {
            foreach (var estate in estates)
            {
                this.LastAddedEstatesViewModels.Add(estate);
            }
        }
    }
}