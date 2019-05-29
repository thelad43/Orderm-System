namespace OrdermSystem.Web.Models.Orders
{
    using System;

    using OrdermSystem.Common.Mapping;
    using OrdermSystem.Data.Models;
    using OrdermSystem.Data.Models.Enums;

    public class OrderViewModel : OrderBaseModel, IMapFrom<PurchaseOrder>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Status Status { get; set; }

        public decimal TotalAmount { get; set; }
    }
}