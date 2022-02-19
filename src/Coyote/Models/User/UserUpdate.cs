using System.ComponentModel.DataAnnotations;

namespace Coyote.Models.User
{
    public class UserUpdate
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CreditCardExpiration { get; set; }

        [Required]
        public string BillingAddress1 { get; set; }

        public string BillingAddress2 { get; set; }

        [Required]
        public string BillingCity { get; set; }

        [Required]
        public string BillingState { get; set; }

        [Required]
        public string BillingZip { get; set; }
    }
}