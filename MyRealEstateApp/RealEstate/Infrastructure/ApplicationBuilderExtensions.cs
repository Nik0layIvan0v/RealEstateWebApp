﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Data;
using RealEstate.Seeder;

namespace RealEstate.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            var database = scopedServices.ServiceProvider.GetService<RealEstateDbContext>();

            database.Database.EnsureDeleted();
            database.Database.Migrate();

            ISeedDatabase seedDatabase = new RealEstateDbContextSeeder(database);

            seedDatabase.Seed();

            return app;
        }
    }
}