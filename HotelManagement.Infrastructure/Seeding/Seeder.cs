using HotelManagement.Core.Domains;
using HotelManagement.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Seeding
{
    public class Seeder
    {
      
        public static async Task SeedData(IApplicationBuilder app)
        {
            //Get db context
            var dbContext = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<HotelDbContext>();
            
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
            if (dbContext.Users.Any())
            {
                return;
            }
            else
            {
                var baseDir = Directory.GetCurrentDirectory();

                await dbContext.Database.EnsureCreatedAsync();
                //Get Usermanager and rolemanager from IoC container
                var userManager = app.ApplicationServices.CreateScope()
                                              .ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var roleManager = app.ApplicationServices.CreateScope()
                                                .ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                List<string> roles = new() { "Admin", "Manager", "Customer" };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

                var user = new AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Chidi",
                    LastName = "Admin",
                    UserName = "Michael",
                    Email = "info@hba.com",
                    PhoneNumber = "09043546576",
                    Gender = "Male",
                    Age = 14,
                    IsActive = true,
                    PublicId = null,
                    Avatar = "http://placehold.it/32x32",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                // user.EmailConfirmed = true;
                await userManager.CreateAsync(user, "Password@123");
                await userManager.AddToRoleAsync(user, "Admin");

                var path = File.ReadAllText(FilePath(baseDir, "JsonFile/Users.json"));
                var customerPath = File.ReadAllText(FilePath(baseDir, "JsonFile/Customers.json"));
                var managerPath = File.ReadAllText(FilePath(baseDir, "JsonFile/Managers.json"));
                var hotelPath = File.ReadAllText(FilePath(baseDir, "JsonFile/Hotels.json"));
                var roomTypePath = File.ReadAllText(FilePath(baseDir, "JsonFile/RoomTypes.json"));
                var bookingPath = File.ReadAllText(FilePath(baseDir, "JsonFile/Bookings.json"));

                var hbaUsers = JsonConvert.DeserializeObject<List<AppUser>>(path);
                var hbaCustomers = JsonConvert.DeserializeObject<List<Customer>>(customerPath);
                var hbaManager = JsonConvert.DeserializeObject<List<Manager>>(managerPath);
                var hbaHotel = JsonConvert.DeserializeObject<List<Hotel>>(hotelPath);
                var hbaRoomType = JsonConvert.DeserializeObject<List<RoomType>>(roomTypePath);
                var hbaBooking = JsonConvert.DeserializeObject<List<Booking>>(bookingPath);

                for (int i = 0; i < hbaUsers.Count; i++)
                {
                    //hbaUsers[i].EmailConfirmed = true;
                    await userManager.CreateAsync(hbaUsers[i], "Password@123");

                    if (i < 5)
                    {
                        await userManager.AddToRoleAsync(hbaUsers[i], "Manager");
                        continue;
                    }
                    await userManager.AddToRoleAsync(hbaUsers[i], "Customer");
                }

                for (int i = 0; i < hbaManager.Count; i++)
                {

                    var manager = new Manager() { Id = hbaManager[i].Id,
                        AppUserId=hbaManager[i].AppUserId,
                        BusinessEmail = hbaManager[i].BusinessEmail,
                        BusinessPhone = hbaManager[i].BusinessPhone,
                        Address = hbaManager[i].Address,
                        State = hbaManager[i].State };

                    await dbContext.Managers.AddAsync(manager);

                }

                for (int i = 0; i < hbaCustomers.Count; i++)
                {

                    var customer = new Customer() { 
                        Id = hbaCustomers[i].Id, 
                        AppUserId = hbaCustomers[i].AppUserId, 
                        CreditCard = hbaCustomers[i].CreditCard,
                        Address = hbaCustomers[i].Address,
                        State = hbaCustomers[i].State };

                    await dbContext.Customers.AddAsync(customer);

                }

                for (int i = 0; i < hbaHotel.Count; i++)
                {
                    var hotel = new Hotel()
                    {
                        Id = hbaHotel[i].Id,
                        Name = hbaHotel[i].Name,
                        Description = hbaHotel[i].Description,
                        Email = hbaHotel[i].Email,
                        Phone = hbaHotel[i].Phone,
                        Address = hbaHotel[i].Address,
                        City = hbaHotel[i].City,
                        State = hbaHotel[i].State,
                        AccountName = hbaHotel[i].AccountName,
                        AccountNumber = hbaHotel[i].AccountNumber,
                        BankName = hbaHotel[i].BankName,
                        BankCode = hbaHotel[i].BankCode,
                        ManagerId = hbaHotel[i].ManagerId,
                        Location = hbaHotel[i].Location,
                        Rating = hbaHotel[i].Rating
                    };
                    await dbContext.Hotels.AddAsync(hotel);

                }

                for (int i = 0; i < hbaRoomType.Count; i++)
                {
                    var roomType = new RoomType()
                    {
                        Id = hbaRoomType[i].Id,
                        HotelId = hbaRoomType[i].HotelId,
                        Name = hbaRoomType[i].Name,
                        Description = hbaRoomType[i].Description,
                        Price = hbaRoomType[i].Price,
                        Discount = hbaRoomType[i].Discount,
                        Thumbnail = hbaRoomType[i].Thumbnail,
                        Available = hbaRoomType[i].Available
                    };
                    await dbContext.RoomTypes.AddAsync(roomType);

                }

                for (var i = 0; i < hbaBooking.Count; i++)
                {
                    var booking = new Booking()
                    {
                        Id = hbaBooking[i].Id,
                        PaymentStatus = hbaBooking[i].PaymentStatus,
                        BookingReference = hbaBooking[i].BookingReference,
                        CheckIn = hbaBooking[i].CheckIn,
                        CheckOut = hbaBooking[i].CheckOut,
                        NoOfPeople = hbaBooking[i].NoOfPeople,
                        ServiceName = hbaBooking[i].ServiceName,
                        HotelId = hbaBooking[i].HotelId,
                        CustomerId = hbaBooking[i].CustomerId,
                        RoomTypeId = hbaBooking[i].RoomTypeId,
                        PaymentId = hbaBooking[i].PaymentId
                    };
                    await dbContext.Bookings.AddAsync(booking);

                }

            }
           
            await dbContext.SaveChangesAsync();
        }

        static string FilePath(string folderName, string fileName)
        {
            return Path.Combine(folderName, fileName);
        }
    }
}
