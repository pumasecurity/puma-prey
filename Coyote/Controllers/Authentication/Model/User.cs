using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coyote.Controllers.Authentication.Model
{
    public class User
    {
        
        public int MemberId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardExpiration { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public List<int> SafariIds { get; set; }
    }
}
