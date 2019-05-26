namespace OrdermSystem.Web.Models.Customers
{
    using System;

    using OrdermSystem.Common.Mapping;
    using OrdermSystem.Data.Models;
    using OrdermSystem.Data.Models.Enums;

    public class CustomerViewModel : IMapFrom<Customer>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsMale { get; set; }

        public string PhoneNumber { get; set; }

        public Status Status { get; set; }
    }
}