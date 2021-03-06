﻿namespace OrdermSystem.Services.SortHelpers.Strategies.PurchaseOrder
{
    using System.Linq;

    using OrdermSystem.Data.Models;

    public class QuantitySortStrategy : ISortStrategy<PurchaseOrder>
    {
        public IQueryable<PurchaseOrder> Sort(IQueryable<PurchaseOrder> elements)
            => elements.OrderBy(po => po.Quantity);
    }
}