namespace OrdermSystem.Services.SortHelpers.Strategies.Customer
{
    using System.Linq;

    using OrdermSystem.Data.Models;

    public class CreatedOnSortStrategy : ISortStrategy<Customer>
    {
        public IQueryable<Customer> Sort(IQueryable<Customer> elements)
            => elements.OrderBy(c => c.CreatedOn);
    }
}