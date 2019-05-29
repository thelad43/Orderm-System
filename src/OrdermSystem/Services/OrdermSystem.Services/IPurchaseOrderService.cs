namespace OrdermSystem.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using OrdermSystem.Data.Models;
    using OrdermSystem.Data.Models.Enums;

    public interface IPurchaseOrderService
    {
        Task<IEnumerable<TModel>> AllAsync<TModel>(int page, string customerId, string sort);

        Task CreateAsync(string description, decimal price, int quantity, string customerId);

        Task<TModel> GetByIdAsync<TModel>(string id);

        Task<PurchaseOrder> GetByIdAsync(string id);

        Task UpdateAsync(string id, string description, decimal price, int quantity, decimal totalAmount, Status status);

        Task DeleteAsync(string id);

        Task<int> CountByCustomerAsync(string customerId);
    }
}