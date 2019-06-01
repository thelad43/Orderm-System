namespace OrdermSystem.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    using OrdermSystem.Common;
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

            await DbInfrastructure.SeedCustomers(100, 10, db);

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

            await DbInfrastructure.SeedCustomers(100, 10, db);

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

            await DbInfrastructure.SeedCustomers(100, 10, db);

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

            await DbInfrastructure.SeedCustomers(100, 10, db);

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

            await DbInfrastructure.SeedCustomers(100, 10, db);

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

            await DbInfrastructure.SeedCustomers(100, 10, db);

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

            await DbInfrastructure.SeedCustomers(100, 10, db);

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

        [Fact]
        public async Task CountAsyncShouldReturnCorrectCountOfNonDeletedCustomers()
        {
            var db = DbInfrastructure.GetDatabase();

            const int Customers = 150;

            await DbInfrastructure.SeedCustomers(Customers, 20, db);

            var serviceProviderMock = new Mock<IServiceProvider>();
            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            var customersCount = await customerService.CountAsync();

            customersCount
                .Should()
                .Be(Customers);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateAndSaveCustomerInDatabase()
        {
            var db = DbInfrastructure.GetDatabase();

            var serviceProviderMock = new Mock<IServiceProvider>();
            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            await customerService.CreateAsync(Constants.FirstName, Constants.LastName, Constants.IsMale, Constants.PhoneNumber);

            var actualCustomer = await db.Customers.FirstOrDefaultAsync();

            actualCustomer
                .Should()
                .Match<Customer>(c => c.FirstName == Constants.FirstName)
                .And
                .Match<Customer>(c => c.LastName == Constants.LastName)
                .And
                .Match<Customer>(c => c.IsMale == Constants.IsMale)
                .And
                .Match<Customer>(c => c.PhoneNumber == Constants.PhoneNumber)
                .And
                .Match<Customer>(c => c.Status == Status.Active);
        }

        [Fact]
        public void DeleteAsyncShouldThrowInvalidOperationExceptionIfCustomerIsNotFound()
        {
            var db = DbInfrastructure.GetDatabase();

            var serviceProviderMock = new Mock<IServiceProvider>();
            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            Func<Task> func = async () => await customerService.DeleteAsync(Guid.NewGuid().ToString());

            func
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(ExceptionMessages.CustomerNotFound);
        }

        [Fact]
        public async Task DeleteAsyncShouldMarkCustomerAsDeleted()
        {
            var db = DbInfrastructure.GetDatabase();

            var customer = new Customer
            {
                FirstName = "Tedd",
                LastName = "Ivanov",
                Status = Status.Active,
                PhoneNumber = "089 456 231",
                IsMale = true,
                CreatedOn = DateTime.UtcNow,
            };

            await db.AddAsync(customer);
            await db.SaveChangesAsync();

            var serviceProviderMock = new Mock<IServiceProvider>();
            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            await customerService.DeleteAsync(customer.Id);

            var actualCustomer = await db.Customers.FirstOrDefaultAsync();

            actualCustomer
                .Status
                .Should()
                .Be(Status.Deleted);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnCustomerByIdToTModel()
        {
            var db = DbInfrastructure.GetDatabase();

            var customer = await DbInfrastructure.SeedCustomer(db);
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));
            var actualCustomer = await customerService.GetByIdAsync<CustomerViewModel>(customer.Id);

            actualCustomer
                .Should()
                .Match<CustomerViewModel>(c => c.FirstName == Constants.FirstName)
                .And
                .Match<CustomerViewModel>(c => c.LastName == Constants.LastName)
                .And
                .Match<CustomerViewModel>(c => c.IsMale == Constants.IsMale)
                .And
                .Match<CustomerViewModel>(c => c.PhoneNumber == Constants.PhoneNumber)
                .And
                .Match<CustomerViewModel>(c => c.Status == Status.Active);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnCorrectCustomer()
        {
            var db = DbInfrastructure.GetDatabase();
            var customer = await DbInfrastructure.SeedCustomer(db);
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));
            var actualCustomer = await customerService.GetByIdAsync(customer.Id);

            actualCustomer
                .Should()
                .Match<Customer>(c => c.FirstName == Constants.FirstName)
                .And
                .Match<Customer>(c => c.LastName == Constants.LastName)
                .And
                .Match<Customer>(c => c.IsMale == Constants.IsMale)
                .And
                .Match<Customer>(c => c.PhoneNumber == Constants.PhoneNumber)
                .And
                .Match<Customer>(c => c.Status == Status.Active);
        }

        [Fact]
        public void UpdateAsyncShouldThrowInvalidOperationExceptionIfCustomerIsNotFound()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            Func<Task> func = async () => await customerService.UpdateAsync(Guid.NewGuid().ToString(), string.Empty, string.Empty, string.Empty, Status.Active);

            func
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(ExceptionMessages.CustomerNotFound);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateCustomer()
        {
            var db = DbInfrastructure.GetDatabase();
            var customer = await DbInfrastructure.SeedCustomer(db);
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customerService = new CustomerService(db, new SortStrategyParser(serviceProviderMock.Object));

            const string UpdatedFirstName = "Teodora";
            const string UpdatedLastName = "Ivanova";
            const string UpdatedPhoneNumer = "08 888 555";

            await customerService.UpdateAsync(customer.Id, UpdatedFirstName, UpdatedLastName, UpdatedPhoneNumer, Status.Inactive);

            var actualCustomer = await db.Customers.FirstOrDefaultAsync();

            actualCustomer
                .Should()
                .Match<Customer>(c => c.FirstName == UpdatedFirstName)
                .And
                .Match<Customer>(c => c.LastName == UpdatedLastName)
                .And
                .Match<Customer>(c => c.PhoneNumber == UpdatedPhoneNumer)
                .And
                .Match<Customer>(c => c.Status == Status.Inactive);
        }
    }
}