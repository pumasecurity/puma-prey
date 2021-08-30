using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Puma.Prey.Rabbit.Models
{
    public class PumaUser : IdentityUser
    {
        public int MemberId { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardExpiration { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public List<SafariUser> SafariUsers { get; set; }
    }
}
