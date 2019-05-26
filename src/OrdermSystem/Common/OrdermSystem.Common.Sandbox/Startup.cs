namespace OrdermSystem.Common.Sandbox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using OrdermSystem.Data;
    using OrdermSystem.Data.Models;
    using OrdermSystem.Data.Models.Enums;

    public class Startup
    {
        private static readonly Random random = new Random();

        public static void Main()
        {
        }

        public static void SeedDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var db = services.GetService<ApplicationDbContext>();

                const int CustomersCount = 200;
                const int PurchaseOrders = 450;

                var customers = SeedCustomers(CustomersCount, db);
                SeedPurchaseOrders(PurchaseOrders, db, customers.ToList());
            }
        }

        private static void SeedPurchaseOrders(int purchaseOrders, ApplicationDbContext db, List<Customer> customers)
        {
            for (var i = 0; i < purchaseOrders; i++)
            {
                var purchaseOrder = new PurchaseOrder
                {
                    Description = $"Some Description {i}",
                    Price = (i + 1) * 100,
                    Quantity = (i + 1) * 2,
                    CreatedOn = DateTime.UtcNow,
                    TotalAmount = (i + 1) * 2 * (i + 1) * 100,
                    CustomerId = customers[random.Next(0, customers.Count)].Id,
                    Status = Status.Active
                };

                db.Add(purchaseOrder);
            }

            db.SaveChanges();
        }

        private static IEnumerable<Customer> SeedCustomers(int customersCount, ApplicationDbContext db)
        {
            var customers = new List<Customer>(customersCount);

            for (var i = 0; i < customersCount; i++)
            {
                var customer = new Customer
                {
                    FirstName = $"First name {i}",
                    LastName = $"Last name {i}",
                    CreatedOn = DateTime.UtcNow,
                    IsMale = random.Next(1, 3) == 1 ? true : false,
                    PhoneNumber = $"{i}{i + 1}{i + 2}{i + 3}",
                    Status = Status.Active
                };

                db.Add(customer);
                customers.Add(customer);
            }

            db.SaveChanges();

            return customers;
        }
    }
}