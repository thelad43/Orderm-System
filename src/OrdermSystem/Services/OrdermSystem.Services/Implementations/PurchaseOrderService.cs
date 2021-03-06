﻿namespace OrdermSystem.Services.Implementations
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
    using OrdermSystem.Services.SortHelpers;

    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly ApplicationDbContext db;
        private readonly ISortStrategyParser strategyParser;

        public PurchaseOrderService(ApplicationDbContext db, ISortStrategyParser strategyParser)
        {
            this.db = db;
            this.strategyParser = strategyParser;
        }

        public async Task<IEnumerable<TModel>> AllAsync<TModel>(int page, string customerId, string sort)
        {
            var orders = this.db
                   .Purchases
                   .Where(c => c.Status != Status.Deleted)
                   .Where(c => c.CustomerId == customerId)
                   .AsQueryable();

            var sortStrategy = this.strategyParser.Parse<PurchaseOrder>(sort);

            if (sortStrategy == null)
            {
                return await orders
                    .OrderByDescending(c => c.CreatedOn)
                    .Skip((page - 1) * WebConstants.OrdersPerPage)
                    .Take(WebConstants.OrdersPerPage)
                    .To<TModel>()
                    .ToListAsync();
            }

            orders = sortStrategy.Sort(orders);

            return await orders
                   .Skip((page - 1) * WebConstants.OrdersPerPage)
                   .Take(WebConstants.OrdersPerPage)
                   .To<TModel>()
                   .ToListAsync();
        }

        public async Task<int> CountByCustomerAsync(string customerId)
            => await this.db
                .Purchases
                .Where(po => po.Status != Status.Deleted)
                .Where(po => po.CustomerId == customerId)
                .CountAsync();

        public async Task CreateAsync(string description, decimal price, int quantity, string customerId)
        {
            var purchaseOrder = new PurchaseOrder
            {
                Description = description,
                Price = price,
                Quantity = quantity,
                TotalAmount = price * quantity,
                CustomerId = customerId,
                Status = Status.Active,
                CreatedOn = DateTime.UtcNow
            };

            await this.db.AddAsync(purchaseOrder);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var order = await this.GetPurchaseOrderById(id);

            if (order == null)
            {
                throw new InvalidOperationException(ExceptionMessages.OrderNotFound);
            }

            order.Status = Status.Deleted;

            await this.db.SaveChangesAsync();
        }

        public async Task<TModel> GetByIdAsync<TModel>(string id)
            => await this.db
                .Purchases
                .Where(po => po.Id == id)
                .To<TModel>()
                .FirstOrDefaultAsync();

        public async Task<PurchaseOrder> GetByIdAsync(string id)
            => await this.db
                .Purchases
                .FirstOrDefaultAsync(po => po.Id == id);

        public async Task UpdateAsync(string id, string description, decimal price, int quantity, Status status)
        {
            var order = await this.GetPurchaseOrderById(id);

            if (order == null)
            {
                throw new InvalidOperationException(ExceptionMessages.OrderNotFound);
            }

            order.Description = description;
            order.Price = price;
            order.Quantity = quantity;
            order.TotalAmount = price * quantity;
            order.Status = status;

            await this.db.SaveChangesAsync();
        }

        private async Task<PurchaseOrder> GetPurchaseOrderById(string id) => await this.db.Purchases.FindAsync(id);
    }
}