namespace OrdermSystem.Web.Models.Customers
{
    using System;

    using OrdermSystem.Common.Mapping;
    using OrdermSystem.Data.Models;

    public class CustomerViewModel : CustomerBaseViewModel, IMapFrom<Customer> 
    {
        public DateTime CreatedOn { get; set; }
    }
}