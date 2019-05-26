namespace OrdermSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using OrdermSystem.Data.Common.Models;
    using OrdermSystem.Data.Models.Enums;

    using static OrdermSystem.Data.Common.DataConstants;

    public class PurchaseOrder : BaseModel<string>
    {
        [Required]
        public string Description { get; set; }

        [Range(MinPrice, MaxPrice)]
        public decimal Price { get; set; }

        [Range(MinQuantity, MaxQuantity)]
        public int Quantity { get; set; }

        [Range(MinAmount, MaxAmount)]
        public decimal TotalAmount { get; set; }

        public string CustomerId { get; set; }

        public Customer Customer { get; set; }

        public Status Status { get; set; }
    }
}