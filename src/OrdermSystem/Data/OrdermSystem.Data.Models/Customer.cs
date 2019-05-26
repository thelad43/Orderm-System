namespace OrdermSystem.Data.Models
{
    using System.Collections.Generic;

    using OrdermSystem.Data.Common.Models;
    using OrdermSystem.Data.Models.Enums;

    public class Customer : BaseModel<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsMale { get; set; }

        public string PhoneNumber { get; set; }

        public Status Status { get; set; }

        public List<PurchaseOrder> Purchases { get; set; } = new List<PurchaseOrder>();
    }
}