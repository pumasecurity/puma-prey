using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Puma.Prey.Skunk.Models
{
    public class AccountMembership : System.Web.Security.SqlMembershipProvider
    {
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            MembershipUser newUser = base.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);

            CreateRabbitRecord(newUser);

            return newUser;
        }

        public bool HasPassword(string username)
        {
            bool hasPassword = false;

            MembershipUser user = Membership.GetUser(username);
            if (user == null)
                return hasPassword;

            hasPassword = !string.IsNullOrEmpty(user.GetPassword());

            return hasPassword;
        }

        private void CreateRabbitRecord(MembershipUser newUser)
        {
            /*
            Puma.Prey.Rabbit.Customer cust = new Puma.Prey.Rabbit.Customer();

            cust.Email = newUser.Email;

            Puma.Prey.Rabbit.DataContext db = new Puma.Prey.Rabbit.DataContext();

            db.Customers.InsertOnSubmit(cust);
            db.SubmitChanges();
            */
        }
    }
}