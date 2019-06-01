namespace OrdermSystem.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using OrdermSystem.Data;
    using OrdermSystem.Data.Models;
    using OrdermSystem.Data.Models.Enums;

    public class DbInfrastructure
    {
        private static Random random = new Random();

        public static ApplicationDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(dbOptions);
        }

        public static async Task SeedCustomers(int customersCount, int deletedCustomers, ApplicationDbContext db)
        {
            for (var i = 0; i < customersCount; i++)
            {
                await db.AddAsync(new Customer
                {
                    FirstName = $"Some name {i}",
                    LastName = $"Some last name {i}",
                    CreatedOn = DateTime.UtcNow,
                    IsMale = random.Next(1, 3) == 1 ? true : false,
                    PhoneNumber = $"{i}{i}{i}",
                    Status = (Status)random.Next(1, 3)
                });
            }

            for (var i = 0; i < deletedCustomers; i++)
            {
                await db.AddAsync(new Customer
                {
                    FirstName = $"Some name {i}",
                    LastName = $"Some last name {i}",
                    CreatedOn = DateTime.UtcNow,
                    IsMale = random.Next(1, 3) == 1 ? true : false,
                    PhoneNumber = $"{i}{i}{i}",
                    Status = Status.Deleted
                });
            }

            await db.SaveChangesAsync();
        }

        public static async Task<Customer> SeedCustomer(ApplicationDbContext db)
        {
            var customer = new Customer
            {
                FirstName = Constants.FirstName,
                LastName = Constants.LastName,
                Status = Status.Active,
                PhoneNumber = Constants.PhoneNumber,
                IsMale = Constants.IsMale,
                CreatedOn = DateTime.UtcNow,
            };

            await db.AddAsync(customer);
            await db.SaveChangesAsync();

            return customer;
        }

        public static async Task SeedOrdersByCustomer(ApplicationDbContext db, Customer customer)
        {
            for (var i = 1; i <= 150; i++)
            {
                var price = i * 10.2M;
                var quantity = i * 12;

                await db.AddAsync(new PurchaseOrder
                {
                    CustomerId = customer.Id,
                    CreatedOn = DateTime.UtcNow,
                    Description = $"Some Descr {i}",
                    Price = price,
                    Quantity = quantity,
                    Status = (Status)random.Next(1, 3),
                    TotalAmount = price * quantity
                });
            }

            for (var i = 1; i < 10; i++)
            {
                var price = i * 10.2M;
                var quantity = i * 12;

                await db.AddAsync(new PurchaseOrder
                {
                    CustomerId = customer.Id,
                    CreatedOn = DateTime.UtcNow,
                    Description = $"Some Descr {i}",
                    Price = price,
                    Quantity = quantity,
                    Status = Status.Deleted,
                    TotalAmount = price * quantity
                });
            }

            await db.SaveChangesAsync();
        }
    }
}