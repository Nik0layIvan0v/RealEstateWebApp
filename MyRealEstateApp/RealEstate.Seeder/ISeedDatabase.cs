using RealEstate.Data;
using System;

namespace RealEstate.Seeder
{
    public interface ISeedDatabase
    {
        void SeedConstantData(RealEstateDbContext Context);

        void SeedAdministrator(IServiceProvider serviceProvider);
    }
}