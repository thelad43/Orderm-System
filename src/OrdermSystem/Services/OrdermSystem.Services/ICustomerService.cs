namespace OrdermSystem.Services
{
    using System.Threading.Tasks;

    using OrdermSystem.Data.Models.Enums;

    public interface ICustomerService
    {
        Task CreateAsync(string firstName, string lastName, bool isMale, string phoneNumber);

        Task<TModel> GetByNameAsync<TModel>(string firstName);

        Task<TModel> GetByIdAsync<TModel>(string id);

        Task UpdateAsync(string id, string firstName, string lastName, string phoneNumber, Status status);

        Task DeleteAsync(string id);
    }
}