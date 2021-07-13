namespace RealEstate.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

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

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Neighborhood> Neighborhoods { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<TradeType> TradeTypes { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FollowerFollowing>()
                .HasKey(x => new { x.FollowerId, x.FollowingId });

            builder.Entity<FollowerFollowing>()
                .HasOne(x => x.Follower)
                .WithMany(x => x.Followers)
                .HasForeignKey(x => x.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FollowerFollowing>()
                .HasOne(x => x.Following)
                .WithMany(x => x.Followings)
                .HasForeignKey(x => x.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Note>()
                .HasKey(x => new { x.EstateId, x.UserId });

            builder.Entity<Note>()
                .HasOne(x => x.Estate)
                .WithMany(x => x.Notes)
                .HasForeignKey(x => x.EstateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Note>()
                .HasOne(x => x.User)
                .WithMany(x => x.Notes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Estate>()
                .HasOne(x => x.Area)
                .WithMany(x => x.Estates)
                .HasForeignKey(x => x.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Estate>()
                .HasOne(x => x.City)
                .WithMany(x => x.Estates)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Report>()
                .HasOne(x => x.ReportingUser)
                .WithMany(x => x.ReportingUsers)
                .HasForeignKey(x => x.ReportingUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Report>()
                .HasOne(x => x.ReportedUser)
                .WithMany(x => x.ReportedUsers)
                .HasForeignKey(x => x.ReportedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
