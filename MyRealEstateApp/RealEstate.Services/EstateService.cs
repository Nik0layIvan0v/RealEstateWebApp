using RealEstate.Data;

namespace RealEstate.Services
{
    public class EstateService
    {
        private readonly RealEstateDbContext Context;

        public EstateService(RealEstateDbContext context)
        {
            this.Context = context;
        }

        public void CreateEstate()
        {
        }
    }
}
