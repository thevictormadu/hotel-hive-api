using HotelManagement.Core.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Context
{
    public class HotelDbContext : IdentityDbContext<User>
    {
       
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Payment> Payments{ get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<WishList> WishLists { get; set; }
      

        public HotelDbContext(DbContextOptions<HotelDbContext> Options) : base(Options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
