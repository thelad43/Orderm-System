namespace OrdermSystem.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    using OrdermSystem.Web.Models.Orders;

    public class PurchaseOrderServiceTests
    {
        public PurchaseOrderServiceTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectOrdersWithoutSort()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customer = await DbInfrastructure.SeedCustomer(db);

            await DbInfrastructure.SeedCustomers(100, 10, db);

            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));

            await DbInfrastructure.SeedOrdersByCustomer(db, customer);

            for (var i = 1; i <= 8; i++)
            {
                var orders = await orderService.AllAsync<OrderViewModel>(i, customer.Id, string.Empty);

                orders
                    .Should()
                    .HaveCount(WebConstants.OrdersPerPage);

                orders
                    .Should()
                    .BeInDescendingOrder(c => c.CreatedOn);

                orders
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByCreatedOn()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customer = await DbInfrastructure.SeedCustomer(db);

            await DbInfrastructure.SeedCustomers(100, 10, db);

            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));

            await DbInfrastructure.SeedOrdersByCustomer(db, customer);

            const string CreatedOn = "createdon";

            for (var i = 1; i <= 8; i++)
            {
                var orders = await orderService.AllAsync<OrderViewModel>(i, customer.Id, CreatedOn);

                orders
                    .Should()
                    .HaveCount(WebConstants.OrdersPerPage);

                orders
                    .Should()
                    .BeInAscendingOrder(c => c.CreatedOn);

                orders
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByDescription()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customer = await DbInfrastructure.SeedCustomer(db);

            await DbInfrastructure.SeedCustomers(100, 10, db);

            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));

            await DbInfrastructure.SeedOrdersByCustomer(db, customer);

            const string Description = "description";

            for (var i = 1; i <= 8; i++)
            {
                var orders = await orderService.AllAsync<OrderViewModel>(i, customer.Id, Description);

                orders
                    .Should()
                    .HaveCount(WebConstants.OrdersPerPage);

                orders
                    .Should()
                    .BeInAscendingOrder(c => c.Description);

                orders
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByPrice()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customer = await DbInfrastructure.SeedCustomer(db);

            await DbInfrastructure.SeedCustomers(100, 10, db);

            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));

            await DbInfrastructure.SeedOrdersByCustomer(db, customer);

            const string Price = "price";

            for (var i = 1; i <= 8; i++)
            {
                var orders = await orderService.AllAsync<OrderViewModel>(i, customer.Id, Price);

                orders
                    .Should()
                    .HaveCount(WebConstants.OrdersPerPage);

                orders
                    .Should()
                    .BeInAscendingOrder(c => c.Price);

                orders
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByQuantity()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customer = await DbInfrastructure.SeedCustomer(db);

            await DbInfrastructure.SeedCustomers(100, 10, db);

            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));

            await DbInfrastructure.SeedOrdersByCustomer(db, customer);

            const string Quantity = "quantity";

            for (var i = 1; i <= 8; i++)
            {
                var orders = await orderService.AllAsync<OrderViewModel>(i, customer.Id, Quantity);

                orders
                    .Should()
                    .HaveCount(WebConstants.OrdersPerPage);

                orders
                    .Should()
                    .BeInAscendingOrder(c => c.Quantity);

                orders
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByStatus()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customer = await DbInfrastructure.SeedCustomer(db);

            await DbInfrastructure.SeedCustomers(100, 10, db);

            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));

            await DbInfrastructure.SeedOrdersByCustomer(db, customer);

            const string StatusSort = "status";

            for (var i = 1; i <= 8; i++)
            {
                var orders = await orderService.AllAsync<OrderViewModel>(i, customer.Id, StatusSort);

                orders
                    .Should()
                    .HaveCount(WebConstants.OrdersPerPage);

                orders
                    .Should()
                    .BeInAscendingOrder(c => c.Status);

                orders
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectCustomersSortedByTotalAmount()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customer = await DbInfrastructure.SeedCustomer(db);

            await DbInfrastructure.SeedCustomers(100, 10, db);

            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));

            await DbInfrastructure.SeedOrdersByCustomer(db, customer);

            const string TotalAmount = "totalamount";

            for (var i = 1; i <= 8; i++)
            {
                var orders = await orderService.AllAsync<OrderViewModel>(i, customer.Id, TotalAmount);

                orders
                    .Should()
                    .HaveCount(WebConstants.OrdersPerPage);

                orders
                    .Should()
                    .BeInAscendingOrder(c => c.TotalAmount);

                orders
                    .Should()
                    .NotContain(c => c.Status == Status.Deleted);
            }
        }

        [Fact]
        public async Task CountByCustomerAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var customer = await DbInfrastructure.SeedCustomer(db);

            await DbInfrastructure.SeedCustomers(100, 10, db);

            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));

            await DbInfrastructure.SeedOrdersByCustomer(db, customer);

            var ordersCountByCustomer = await orderService.CountByCustomerAsync(customer.Id);

            ordersCountByCustomer
                .Should()
                .Be(150);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateAndSaveOrderInDatabase()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));
            var customer = await DbInfrastructure.SeedCustomer(db);

            await orderService.CreateAsync(Constants.Description, Constants.Price, Constants.Quantity, customer.Id);

            var order = await db.Purchases.FirstOrDefaultAsync(po => po.CustomerId == customer.Id);

            order
                .Should()
                .Match<PurchaseOrder>(po => po.Description == Constants.Description)
                .And
                .Match<PurchaseOrder>(po => po.Price == Constants.Price)
                .And
                .Match<PurchaseOrder>(po => po.Quantity == Constants.Quantity)
                .And
                .Match<PurchaseOrder>(po => po.Status == Status.Active);
        }

        [Fact]
        public void DeleteAsyncShouldThrowInvalidOperationExceptionIfOrderIsNotFound()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));

            Func<Task> func = async () => await orderService.DeleteAsync(Guid.NewGuid().ToString());

            func
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(ExceptionMessages.OrderNotFound);
        }

        [Fact]
        public async Task DeleteAsyncShouldMarkOrderAsDeleted()
        {
            var db = DbInfrastructure.GetDatabase();
            var customer = await DbInfrastructure.SeedCustomer(db);

            await DbInfrastructure.SeedOrdersByCustomer(db, customer);

            var serviceProviderMock = new Mock<IServiceProvider>();
            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));

            IEnumerable<OrderViewModel> orders;

            for (var i = 1; i <= 10; i++)
            {
                orders = await orderService.AllAsync<OrderViewModel>(i, customer.Id, string.Empty);
                await orderService.DeleteAsync(orders.First().Id);
            }

            for (var i = 1; i <= 9; i++)
            {
                orders = await orderService.AllAsync<OrderViewModel>(i, customer.Id, string.Empty);

                orders
                    .Should()
                    .HaveCount(WebConstants.OrdersPerPage);
            }
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnCorrectOrderToTModel()
        {
            var db = DbInfrastructure.GetDatabase();
            var customer = await DbInfrastructure.SeedCustomer(db);
            var serviceProviderMock = new Mock<IServiceProvider>();
            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));
            var order = await SeedOrderByCustomer(db, customer);
            var actualOrder = await orderService.GetByIdAsync<OrderViewModel>(order.Id);

            actualOrder
                .Should()
                .Match<OrderViewModel>(po => po.CustomerId == customer.Id)
                .And
                .Match<OrderViewModel>(po => po.Status == Status.Active)
                .And
                .Match<OrderViewModel>(po => po.Description == Constants.Description)
                .And
                .Match<OrderViewModel>(po => po.Price == Constants.Price)
                .And
                .Match<OrderViewModel>(po => po.Quantity == Constants.Quantity)
                .And
                .Match<OrderViewModel>(po => po.TotalAmount == Constants.Price * Constants.Quantity);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnCorrectOrder()
        {
            var db = DbInfrastructure.GetDatabase();
            var customer = await DbInfrastructure.SeedCustomer(db);
            var serviceProviderMock = new Mock<IServiceProvider>();
            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));
            var order = await SeedOrderByCustomer(db, customer);
            var actualOrder = await orderService.GetByIdAsync(order.Id);

            actualOrder
                .Should()
                .Match<PurchaseOrder>(po => po.CustomerId == customer.Id)
                .And
                .Match<PurchaseOrder>(po => po.Status == Status.Active)
                .And
                .Match<PurchaseOrder>(po => po.Description == Constants.Description)
                .And
                .Match<PurchaseOrder>(po => po.Price == Constants.Price)
                .And
                .Match<PurchaseOrder>(po => po.Quantity == Constants.Quantity)
                .And
                .Match<PurchaseOrder>(po => po.TotalAmount == Constants.Price * Constants.Quantity);
        }

        [Fact]
        public void UpdateAsyncShouldThrowInvalidOperationExceptionIfOrderIsNotFound()
        {
            var db = DbInfrastructure.GetDatabase();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));

            Func<Task> func = async () => await orderService.DeleteAsync(Guid.NewGuid().ToString());

            func
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage(ExceptionMessages.OrderNotFound);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateOrder()
        {
            var db = DbInfrastructure.GetDatabase();
            var customer = await DbInfrastructure.SeedCustomer(db);
            var serviceProviderMock = new Mock<IServiceProvider>();
            var orderService = new PurchaseOrderService(db, new SortStrategyParser(serviceProviderMock.Object));
            var order = await SeedOrderByCustomer(db, customer);

            const string UpdatedDescription = "Updated Description!";
            const decimal UpdatedPrice = 999.999M;
            const int UpdatedQuantity = 5;

            await orderService.UpdateAsync(order.Id, UpdatedDescription, UpdatedPrice, UpdatedQuantity, Status.Inactive);

            var actualOrder = await db.Purchases.FirstOrDefaultAsync();

            actualOrder
                .Should()
                .Match<PurchaseOrder>(po => po.Description == UpdatedDescription)
                .And
                .Match<PurchaseOrder>(po => po.Price == UpdatedPrice)
                .And
                .Match<PurchaseOrder>(po => po.Quantity == UpdatedQuantity)
                .And
                .Match<PurchaseOrder>(po => po.Status == Status.Inactive);
        }

        private static async Task<PurchaseOrder> SeedOrderByCustomer(Data.ApplicationDbContext db, Customer customer)
        {
            var order = new PurchaseOrder
            {
                CustomerId = customer.Id,
                Description = Constants.Description,
                Price = Constants.Price,
                Quantity = Constants.Quantity,
                TotalAmount = Constants.Price * Constants.Quantity,
                CreatedOn = DateTime.UtcNow,
                Status = Status.Active,
            };

            await db.AddAsync(order);
            await db.SaveChangesAsync();

            return order;
        }
    }
}