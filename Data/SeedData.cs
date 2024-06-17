using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OfficeSuppliesManagement.Models;
using System;
using System.Linq;

namespace OfficeSuppliesManagement.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any users.
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                context.Users.AddRange(
                    new User
                    {
                        UserId = "user1",
                        Password = "password1",
                        Name = "John Doe",
                        Gender = "Male",
                        BirthDate = DateTime.Parse("1980-01-01"),
                        PhoneNumber = "1234567890"
                    },
                    new User
                    {
                        UserId = "user2",
                        Password = "password2",
                        Name = "Jane Smith",
                        Gender = "Female",
                        BirthDate = DateTime.Parse("1990-02-02"),
                        PhoneNumber = "0987654321"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
