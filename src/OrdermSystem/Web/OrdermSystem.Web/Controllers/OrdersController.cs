namespace OrdermSystem.Web.Controllers
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
        public async Task<IActionResult> Index(string customerId, string sort = null, int id = 1)
        {
            sort = sort ?? string.Empty;

            ViewData[WebConstants.ViewDataSortKey] = sort;

            var orders = await this.orders.AllAsync<OrderViewModel>(id, customerId, sort.ToLower());

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

            await this.orders.UpdateAsync(id, order.Description, order.Price, order.Quantity, Status.Active);

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

            await this.orders.UpdateAsync(id, order.Description, order.Price, order.Quantity, Status.Inactive);

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

        [HttpGet]
        public IActionResult Create(string customerId)
            => View(new OrderBaseViewModel
            {
                CustomerId = customerId
            });

        [HttpPost]
        public async Task<IActionResult> Create(OrderBaseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.orders.CreateAsync(model.Description, model.Price, model.Quantity, model.CustomerId);

            TempData.AddSuccessMessage("Successfully created new order!");

            return this.RedirectToCustomAction(
                nameof(Index),
                nameof(OrdersController), new { customerId = model.CustomerId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var order = await this.orders.GetByIdAsync<OrderEditViewModel>(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderEditViewModel model)
        {
            var order = await this.orders.GetByIdAsync(model.Id);

            if (order == null)
            {
                return NotFound();
            }

            await this.orders.UpdateAsync(model.Id, model.Description, model.Price, model.Quantity, order.Status);

            TempData.AddSuccessMessage($"Successfully updated order!");

            return this.RedirectToCustomAction(
                nameof(Index),
                nameof(OrdersController),
                new { customerId = order.CustomerId });
        }
    }
}