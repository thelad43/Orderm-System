namespace OrdermSystem.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using OrdermSystem.Common;
    using OrdermSystem.Common.Mapping;
    using OrdermSystem.Data;
    using OrdermSystem.Data.Models;
    using OrdermSystem.Data.Models.Enums;

    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext db;

        public CustomerService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<TModel>> AllAsync<TModel>(int page)
            => await this.db
                .Customers
                .Where(c => c.Status != Status.Deleted)
                .OrderByDescending(c => c.CreatedOn)
                .Skip((page - 1) * WebConstants.CustomersPerPage)
                .Take(WebConstants.CustomersPerPage)
                .To<TModel>()
                .ToListAsync();

        public async Task<int> CountAsync()
            => await this.db
                .Customers
                .Where(c => c.Status != Status.Deleted)
                .CountAsync();

        public async Task CreateAsync(string firstName, string lastName, bool isMale, string phoneNumber)
        {
            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                IsMale = isMale,
                PhoneNumber = phoneNumber,
                Status = Status.Active,
                CreatedOn = DateTime.UtcNow
            };

            await this.db.AddAsync(customer);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var customer = await this.GetCustomerById(id);

            if (customer == null)
            {
                throw new InvalidOperationException(ExceptionMessages.CustomerNotFound);
            }

            customer.Status = Status.Deleted;

            await this.db.SaveChangesAsync();
        }

        public async Task<TModel> GetByIdAsync<TModel>(string id)
            => await this.db
            .Customers
            .Where(c => c.Id == id)
            .To<TModel>()
            .FirstOrDefaultAsync();

        public async Task<Customer> GetByIdAsync(string id)
            => await this.db
                .Customers
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<TModel> GetByNameAsync<TModel>(string firstName)
            => await this.db
                .Customers
                .Where(c => c.FirstName.ToLower().Contains(firstName.ToLower()))
                .To<TModel>()
                .FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, string firstName, string lastName, string phoneNumber, Status status)
        {
            var customer = await this.GetCustomerById(id);

            if (customer == null)
            {
                throw new InvalidOperationException(ExceptionMessages.CustomerNotFound);
            }

            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.PhoneNumber = phoneNumber;
            customer.Status = status;

            await this.db.SaveChangesAsync();
        }

        private async Task<Customer> GetCustomerById(string id) => await this.db.Customers.FindAsync(id);
    }
}