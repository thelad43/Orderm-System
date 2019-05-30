namespace OrdermSystem.Web.Models.Orders
{
    using System.ComponentModel.DataAnnotations;

    using OrdermSystem.Common.Mapping;
    using OrdermSystem.Data.Models;

    using static OrdermSystem.Data.Common.DataConstants;

    public class OrderBaseViewModel : IMapFrom<PurchaseOrder>
    {
        [Required]
        public string Description { get; set; }

        [Range(MinPrice, MaxPrice)]
        public decimal Price { get; set; }

        [Range(MinQuantity, MaxQuantity)]
        public int Quantity { get; set; }

        public string CustomerId { get; set; }
    }
}