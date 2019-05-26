﻿namespace OrdermSystem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using OrdermSystem.Common;
    using OrdermSystem.Data.Models.Enums;
    using OrdermSystem.Services;
    using OrdermSystem.Web.Infrastructure.Extensions;
    using OrdermSystem.Web.Models.Orders;

    public class OrdersController : Controller
    {
        private readonly IPurchaseOrderService orders;
        private readonly ICustomerService customers;

        public OrdersController(IPurchaseOrderService orders, ICustomerService customers)
        {
            this.orders = orders;
            this.customers = customers;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string customerId, int id = 1)
        {
            var orders = await this.orders.AllAsync<OrderViewModel>(id, customerId);

            var ordersCount = await this.orders.CountByCustomerAsync(customerId);

            var customer = await this.customers.GetByIdAsync(customerId);

            var model = new OrdersListingViewModel
            {
                Orders = orders,
                CurrentPage = id,
                OrdersCount = ordersCount,
                PagesCount = (int)Math.Ceiling(ordersCount / (decimal)WebConstants.OrdersPerPage),
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                CustomerId = customerId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Activate(string id)
        {
            var order = await this.orders.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            await this.orders.UpdateAsync(id, order.Description, order.Price, order.Quantity, order.TotalAmount, Status.Active);

            TempData.AddSuccessMessage("Status successfully changed to active!");

            return this.RedirectToCustomAction(
                 nameof(Index),
                 nameof(OrdersController),
                 new { customerId = order.CustomerId });
        }

        [HttpPost]
        public async Task<IActionResult> Deactivate(string id)
        {
            var order = await this.orders.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            await this.orders.UpdateAsync(id, order.Description, order.Price, order.Quantity, order.TotalAmount, Status.Inactive);

            TempData.AddSuccessMessage("Status successfully changed to inactive!");

            return this.RedirectToCustomAction(
                 nameof(Index),
                 nameof(OrdersController),
                 new { customerId = order.CustomerId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await this.orders.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            await this.orders.DeleteAsync(id);

            TempData.AddSuccessMessage("Successfully deleted order!");

            return this.RedirectToCustomAction(
                 nameof(Index),
                 nameof(OrdersController),
                 new { customerId = order.CustomerId });
        }
    }
}