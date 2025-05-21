using System;
using System.Web.UI;

namespace Skunk
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileName = Request["fileName"];
            Response.WriteFile(fileName);
        }
    }
}