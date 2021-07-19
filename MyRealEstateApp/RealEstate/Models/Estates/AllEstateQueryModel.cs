﻿using System.Collections.Generic;
using RealEstate.Services.Models;

namespace RealEstate.Models.Estates
{
    public class AllEstateQueryModel
    {
        public int EstatesPerPage { get; set; }= 3;

        public int CurrentPage { get; set; } = 1;

        public int TotalEstates { get; set; }

        public IEnumerable<EstateListingViewModel> EstateListingViewModels { get; set; }
    }
}