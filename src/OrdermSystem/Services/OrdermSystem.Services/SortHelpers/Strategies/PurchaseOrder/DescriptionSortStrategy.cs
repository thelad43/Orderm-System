namespace OrdermSystem.Services.SortHelpers.Strategies.PurchaseOrder
{
    using System.Linq;

    using OrdermSystem.Data.Models;

    public class DescriptionSortStrategy : ISortStrategy<PurchaseOrder>
    {
        public IQueryable<PurchaseOrder> Sort(IQueryable<PurchaseOrder> elements)
            => elements.OrderBy(po => po.Description);
    }
}