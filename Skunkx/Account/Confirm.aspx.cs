using System;
using System.Web;
using System.Web.UI;
using Puma.Prey.Skunk.Models;

namespace Puma.Prey.Skunk.Account
{
    public partial class Confirm : Page
    {
        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Needs to have custom membership code added to the Puma.Prey.Skunk.Models.AccountMembership for verifying a login

            /*
            string code = IdentityHelper.GetCodeFromRequest(Request);
            string userId = IdentityHelper.GetUserIdFromRequest(Request);
            if (code != null && userId != null)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var result = manager.ConfirmEmail(userId, code);
                if (result.Succeeded)
                {
                    successPanel.Visible = true;
                    return;
                }
            }
            */
            successPanel.Visible = false;
            errorPanel.Visible = true;
        }
    }
}