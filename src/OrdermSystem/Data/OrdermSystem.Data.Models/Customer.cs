namespace OrdermSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using OrdermSystem.Data.Common.Models;
    using OrdermSystem.Data.Models.Enums;

    using static OrdermSystem.Data.Common.DataConstants;

    public class Customer : BaseModel<string>
    {
        [Required]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(LastNameMinLength)]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        public bool IsMale { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public Status Status { get; set; }

        public List<PurchaseOrder> Purchases { get; set; } = new List<PurchaseOrder>();
    }
}