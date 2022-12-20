using HotelManagement.Core.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Context
{
    public class HotelDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<WishList> WishLists { get; set; }

        public HotelDbContext(DbContextOptions<HotelDbContext> Options) : base(Options)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        item.Entity.Id = Guid.NewGuid().ToString();
                        item.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var decimalProps = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => (Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

            foreach (var property in decimalProps)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WishList>()
                .HasKey(bc => new { bc.CustomerId, bc.HotelId });
            modelBuilder.Entity<WishList>()
                .HasOne(bc => bc.Customer)
                .WithMany(b => b.WishLists)
                .HasForeignKey(bc => bc.CustomerId);
            modelBuilder.Entity<WishList>()
                .HasOne(bc => bc.Hotel)
                .WithMany(c => c.WishLists)
                .HasForeignKey(bc => bc.HotelId);
        }

    }
}
