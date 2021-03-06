﻿namespace OrdermSystem.Web.Controllers
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

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CustomerBaseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.customers.CreateAsync(model.FirstName, model.LastName, model.IsMale, model.PhoneNumber);

            TempData.AddSuccessMessage("Successfully created new customer!");

            return this.RedirectToCustomAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var customer = await this.customers.GetByIdAsync<CustomerBaseViewModel>(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerBaseViewModel model)
        {
            var customer = await this.customers.GetByIdAsync(model.Id);

            if (customer == null)
            {
                return NotFound();
            }

            await this.customers.UpdateAsync(model.Id, model.FirstName, model.LastName, model.PhoneNumber, customer.Status);

            TempData.AddSuccessMessage($"Successfully updated customer {model.FirstName}!");

            return this.RedirectToCustomAction(nameof(Index));
        }
    }
}