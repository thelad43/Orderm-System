namespace OrdermSystem.Services.Implementations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using OrdermSystem.Common;
    using OrdermSystem.Common.Mapping;
    using OrdermSystem.Data;
    using OrdermSystem.Data.Models;
    using OrdermSystem.Data.Models.Enums;

    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly ApplicationDbContext db;

        public PurchaseOrderService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string description, decimal price, int quantity, decimal totalAmount, string customerId)
        {
            var purchaseOrder = new PurchaseOrder
            {
                Description = description,
                Price = price,
                Quantity = quantity,
                TotalAmount = totalAmount,
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

        public async Task UpdateAsync(string id, string description, decimal price, int quantity, decimal totalAmount, Status status)
        {
            var order = await this.GetPurchaseOrderById(id);

            if (order == null)
            {
                throw new InvalidOperationException(ExceptionMessages.OrderNotFound);
            }

            order.Description = description;
            order.Price = price;
            order.Quantity = quantity;
            order.TotalAmount = totalAmount;
            order.Status = status;

            await this.db.SaveChangesAsync();
        }

        private async Task<PurchaseOrder> GetPurchaseOrderById(string id) => await this.db.Purchases.FindAsync(id);
    }
}