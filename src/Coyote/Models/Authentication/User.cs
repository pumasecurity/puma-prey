using Puma.Prey.Rabbit.Models;
using System.Collections.Generic;

namespace Coyote.Models.Authentication
{
    public class User
    {
        public User() { }

        public int MemberId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardExpiration { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public List<int> SafariIds { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}