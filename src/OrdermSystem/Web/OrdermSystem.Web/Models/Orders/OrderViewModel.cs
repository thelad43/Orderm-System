namespace OrdermSystem.Web.Models.Orders
{
    using System;

    using OrdermSystem.Common.Mapping;
    using OrdermSystem.Data.Models;
    using OrdermSystem.Data.Models.Enums;

    public class OrderViewModel : IMapFrom<PurchaseOrder>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedOn { get; set; }

        public Status Status { get; set; }
    }
}