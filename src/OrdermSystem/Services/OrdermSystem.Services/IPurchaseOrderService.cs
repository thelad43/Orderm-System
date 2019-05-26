namespace OrdermSystem.Services
{
    using System.Threading.Tasks;

    using OrdermSystem.Data.Models.Enums;

    public interface IPurchaseOrderService
    {
        Task CreateAsync(string description, decimal price, int quantity, decimal totalAmount, string customerId);

        Task<TModel> GetByIdAsync<TModel>(string id);

        Task UpdateAsync(string id, string description, decimal price, int quantity, decimal totalAmount, Status status);

        Task DeleteAsync(string id);
    }
}