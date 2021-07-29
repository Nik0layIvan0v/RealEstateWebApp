namespace RealEstate.Infrastructure
{
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Seeder;
    using System;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            RealEstateDbContext database = serviceProvider.GetRequiredService<RealEstateDbContext>();

            //database.Database.EnsureDeleted();
            database.Database.Migrate();

            var databaseSeeder = new RealEstateConstantsSeeder();

            databaseSeeder.SeedConstantData(database);

            databaseSeeder.SeedAdministrator(serviceProvider);

            return app;
        }
    }
}