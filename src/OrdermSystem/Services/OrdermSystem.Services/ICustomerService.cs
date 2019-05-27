namespace OrdermSystem.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using OrdermSystem.Data.Models;
    using OrdermSystem.Data.Models.Enums;

    public interface ICustomerService
    {
        Task<IEnumerable<TModel>> AllAsync<TModel>(int page, string sort);

        Task CreateAsync(string firstName, string lastName, bool isMale, string phoneNumber);

        Task<TModel> GetByNameAsync<TModel>(string firstName);

        Task<TModel> GetByIdAsync<TModel>(string id);

        Task<Customer> GetByIdAsync(string id);

        Task UpdateAsync(string id, string firstName, string lastName, string phoneNumber, Status status);

        Task DeleteAsync(string id);

        Task<int> CountAsync();
    }
}