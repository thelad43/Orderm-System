namespace OrdermSystem.Web.Models.Orders
{
    using System.ComponentModel.DataAnnotations;

    using static OrdermSystem.Data.Common.DataConstants;

    public class OrderBaseModel
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