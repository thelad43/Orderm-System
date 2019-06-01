namespace OrdermSystem.Web.Models.Customers
{
    using System.Collections.Generic;

    public class CustomersListingViewModel
    {
        public IEnumerable<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int CustomersCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;
    }
}