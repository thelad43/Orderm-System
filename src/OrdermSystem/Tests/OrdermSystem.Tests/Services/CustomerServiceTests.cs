namespace OrdermSystem.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FluentAssertions;
    using Moq;
    using Xunit;

    using OrdermSystem.Common;
    using OrdermSystem.Data;
    using OrdermSystem.Data.Models;
    using OrdermSystem.Data.Models.Enums;
    using OrdermSystem.Services.Implementations;
    using OrdermSystem.Services.SortHelpers;
    using OrdermSystem.Web.Models.Customers;

    public class CustomerServiceTests
    {
        public CustomerServiceTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersWithoutSort()
        {
            var db = DbInfrastructure.GetDatabase();

            await this.SeedCustomers(100, 10, db);

            var serviceProviderMock = new Mock<IServiceProvider>();

            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            IEnumerable<CustomerViewModel> customers;

            for (var i = 1; i <= 8; i++)
            {
                customers = await customerService.AllAsync<CustomerViewModel>(i, string.Empty);

                customers
                    .Should()
                    .HaveCount(WebConstants.CustomersPerPage);

                customers
                    .Should()
                    .BeInDescendingOrder(c => c.CreatedOn);

                customers
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }

            customers = await customerService.AllAsync<CustomerViewModel>(9, string.Empty);

            customers
                .Should()
                .HaveCount(4);
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByFirstName()
        {
            var db = DbInfrastructure.GetDatabase();

            await this.SeedCustomers(100, 10, db);

            var serviceProviderMock = new Mock<IServiceProvider>();

            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            IEnumerable<CustomerViewModel> customers;

            const string FirstName = "firstname";

            for (var i = 1; i <= 8; i++)
            {
                customers = await customerService.AllAsync<CustomerViewModel>(i, FirstName);

                customers
                    .Should()
                    .HaveCount(WebConstants.CustomersPerPage);

                customers
                    .Should()
                    .BeInAscendingOrder(c => c.FirstName);

                customers
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }

            customers = await customerService.AllAsync<CustomerViewModel>(9, FirstName);

            customers
                .Should()
                .HaveCount(4);
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByLastName()
        {
            var db = DbInfrastructure.GetDatabase();

            await this.SeedCustomers(100, 10, db);
            var serviceProviderMock = new Mock<IServiceProvider>();

            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            IEnumerable<CustomerViewModel> customers;

            const string LastName = "lastname";

            for (var i = 1; i <= 8; i++)
            {
                customers = await customerService.AllAsync<CustomerViewModel>(i, LastName);

                customers
                    .Should()
                    .HaveCount(WebConstants.CustomersPerPage);

                customers
                    .Should()
                    .BeInAscendingOrder(c => c.LastName);

                customers
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }

            customers = await customerService.AllAsync<CustomerViewModel>(9, LastName);

            customers
                .Should()
                .HaveCount(4);
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByGender()
        {
            var db = DbInfrastructure.GetDatabase();

            await this.SeedCustomers(100, 10, db);
            var serviceProviderMock = new Mock<IServiceProvider>();

            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            IEnumerable<CustomerViewModel> customers;

            const string Gender = "gender";

            for (var i = 1; i <= 8; i++)
            {
                customers = await customerService.AllAsync<CustomerViewModel>(i, Gender);

                customers
                    .Should()
                    .HaveCount(WebConstants.CustomersPerPage);

                customers
                    .Should()
                    .BeInAscendingOrder(c => c.IsMale);

                customers
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }

            customers = await customerService.AllAsync<CustomerViewModel>(9, Gender);

            customers
                .Should()
                .HaveCount(4);
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByPhoneNumber()
        {
            var db = DbInfrastructure.GetDatabase();

            await this.SeedCustomers(100, 10, db);

            var serviceProviderMock = new Mock<IServiceProvider>();

            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            IEnumerable<CustomerViewModel> customers;

            const string PhoneNumer = "phonenumber";

            for (var i = 1; i <= 8; i++)
            {
                customers = await customerService.AllAsync<CustomerViewModel>(i, PhoneNumer);

                customers
                    .Should()
                    .HaveCount(WebConstants.CustomersPerPage);

                customers
                    .Should()
                    .BeInAscendingOrder(c => c.PhoneNumber);

                customers
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }

            customers = await customerService.AllAsync<CustomerViewModel>(9, PhoneNumer);

            customers
                .Should()
                .HaveCount(4);
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByCreatedOn()
        {
            var db = DbInfrastructure.GetDatabase();

            await this.SeedCustomers(100, 10, db);
            var serviceProviderMock = new Mock<IServiceProvider>();

            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            IEnumerable<CustomerViewModel> customers;

            const string CreatedOn = "createdon";

            for (var i = 1; i <= 8; i++)
            {
                customers = await customerService.AllAsync<CustomerViewModel>(i, CreatedOn);

                customers
                    .Should()
                    .HaveCount(WebConstants.CustomersPerPage);

                customers
                    .Should()
                    .BeInAscendingOrder(c => c.CreatedOn);

                customers
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }

            customers = await customerService.AllAsync<CustomerViewModel>(9, CreatedOn);

            customers
                .Should()
                .HaveCount(4);
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByStatus()
        {
            var db = DbInfrastructure.GetDatabase();

            await this.SeedCustomers(100, 10, db);

            var serviceProviderMock = new Mock<IServiceProvider>();

            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            IEnumerable<CustomerViewModel> customers;

            const string Status = "status";

            for (var i = 1; i <= 8; i++)
            {
                customers = await customerService.AllAsync<CustomerViewModel>(i, Status);

                customers
                    .Should()
                    .HaveCount(WebConstants.CustomersPerPage);

                customers
                    .Should()
                    .BeInAscendingOrder(c => c.Status);

                customers
                    .Should()
                    .NotContain(c => c.Status == Data.Models.Enums.Status.Deleted);
            }

            customers = await customerService.AllAsync<CustomerViewModel>(9, Status);

            customers
                .Should()
                .HaveCount(4);
        }

        private async Task SeedCustomers(int customersCount, int deletedCustomers, ApplicationDbContext db)
        {
            var random = new Random();

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
    }
}