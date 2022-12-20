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

                var user = new AppUser
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
                user.EmailConfirmed = true;
                await userManager.CreateAsync(user, "Password@123");
                await userManager.AddToRoleAsync(user, "Admin");

                var path = File.ReadAllText(FilePath(baseDir, "JsonFile/Users.json"));

                var hbaUsers = JsonConvert.DeserializeObject<List<AppUser>>(path);
                for (int i = 0; i < hbaUsers.Count; i++)
                {
                    hbaUsers[i].EmailConfirmed = true;
                    await userManager.CreateAsync(hbaUsers[i], "Password@123");
                    if (i < 5)
                    {
                        await userManager.AddToRoleAsync(hbaUsers[i], "Manager");
                        continue;
                    }
                    await userManager.AddToRoleAsync(hbaUsers[i], "Customer");
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
