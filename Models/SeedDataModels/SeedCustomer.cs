using LibraryAssistant.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LibraryAssistant.Models.SeedDataModels
{
    public static class SeedCustomer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LibraryContext>>()))
            {
                // Look for any movies.
                if (context.Customer.Any())
                {
                    return;   // DB has been seeded
                }

                context.Customer.AddRange(
                    new Customer
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        BirthDate = DateTime.Parse("1992-2-28"),
                        Phone = "0892551537",
                        Email = "j.doe@email.com",
                        DueTotal = 0.00M,
                        Deleted = false
                    },

                    new Customer
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        BirthDate = DateTime.Parse("1989-12-14"),
                        Phone = "0884691025",
                        Email = "janeD@email.com",
                        DueTotal = 0.00M,
                        Deleted = false
                    },

                    new Customer
                    {
                       FirstName = "Mary",
                        LastName = "Sue",
                        BirthDate = DateTime.Parse("1998-1-5"),
                        Phone = "0872526125",
                        Email = "msue@email.com",
                        DueTotal = 0.00M,
                        Deleted = false
                    }
                );
                context.SaveChanges();
            }
        }
    }
}