namespace OrdermSystem.Web.Models.Customers
{
    using System.ComponentModel.DataAnnotations;

    using OrdermSystem.Data.Models.Enums;

    using static OrdermSystem.Data.Common.DataConstants;

    public class CustomerFormViewModel
    {
        [Required]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(LastNameMinLength)]
        [MaxLength(LastNameMaxLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Please select gender")]
        public bool IsMale { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public Status Status { get; set; }
    }
}