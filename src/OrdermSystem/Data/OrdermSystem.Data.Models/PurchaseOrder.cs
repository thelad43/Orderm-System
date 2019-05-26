namespace OrdermSystem.Data.Models
{
    using OrdermSystem.Data.Common.Models;
    using OrdermSystem.Data.Models.Enums;

    public class PurchaseOrder : BaseModel<string>
    {
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int TotalAmount { get; set; }

        public string CustomerId { get; set; }

        public Customer Customer { get; set; }

        public Status Status { get; set; }
    }
}