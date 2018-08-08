using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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