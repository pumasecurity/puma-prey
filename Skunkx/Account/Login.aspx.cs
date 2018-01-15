using System;
using System.Web;
using System.Web.UI;
using Puma.Prey.Skunk.Models;
using System.Web.Security;

namespace Puma.Prey.Skunk.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LoginUser_LoggedIn(object sender, EventArgs e)
        {
            if (Request.QueryString["ReturnUrl"] != null)
                Response.Redirect(Request.QueryString["ReturnUrl"]);
        }
    }
}