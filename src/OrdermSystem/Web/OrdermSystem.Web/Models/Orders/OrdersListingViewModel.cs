namespace OrdermSystem.Web.Models.Orders
{
    using System.Collections.Generic;

    public class OrdersListingViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int OrdersCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;

        public string CustomerName { get; set; }

        public string CustomerId { get; set; }
    }
}