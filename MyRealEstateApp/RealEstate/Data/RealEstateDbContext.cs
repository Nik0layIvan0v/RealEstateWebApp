using RealEstate.Models;

namespace RealEstate.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class RealEstateDbContext : IdentityDbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Estate> Estates { get; set; }

        public DbSet<EstateType> EstateTypes { get; set; }

        public DbSet<UserEstateWishList> EstateWishLists { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<FollowerFollowing> FollowerFollowings { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Neighborhood> Neighborhoods { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<TradeType> TradeTypes { get; set; }
        
        public DbSet<Note> Notes { get; set; }
    }
}
