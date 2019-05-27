namespace OrdermSystem.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using OrdermSystem.Common;
    using OrdermSystem.Data.Models.Enums;
    using OrdermSystem.Services;
    using OrdermSystem.Web.Infrastructure.Extensions;
    using OrdermSystem.Web.Models.Customers;

    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id = 1, string sort = null)
        {
            sort = sort ?? string.Empty;

            ViewData[WebConstants.ViewDataSortKey] = sort;

            var customers = await this.customers.AllAsync<CustomerViewModel>(id, sort.ToLower());

            var customersCount = await this.customers.CountAsync();

            var model = new CustomersListingViewModel
            {
                Customers = customers,
                CurrentPage = id,
                CustomersCount = customersCount,
                PagesCount = (int)Math.Ceiling(customersCount / (decimal)WebConstants.CustomersPerPage)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Activate(string id)
        {
            var customer = await this.customers.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            await this.customers.UpdateAsync(id, customer.FirstName, customer.LastName, customer.PhoneNumber, Status.Active);

            TempData.AddSuccessMessage("Status successfully changed to active!");

            return this.RedirectToCustomAction(nameof(Index), nameof(CustomersController));
        }

        [HttpPost]
        public async Task<IActionResult> Deactivate(string id)
        {
            var customer = await this.customers.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            await this.customers.UpdateAsync(id, customer.FirstName, customer.LastName, customer.PhoneNumber, Status.Inactive);

            TempData.AddSuccessMessage("Status successfully changed to inactive!");

            return this.RedirectToCustomAction(nameof(Index), nameof(CustomersController));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await this.customers.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            await this.customers.DeleteAsync(id);

            TempData.AddSuccessMessage("Successfully deleted customer!");

            return this.RedirectToCustomAction(nameof(Index), nameof(CustomersController));
        }
    }
}