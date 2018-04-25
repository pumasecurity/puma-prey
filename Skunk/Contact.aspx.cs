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

            using (Stream stream2 = new FileStream(fileName, FileMode.Open))
            {
                //Do some stuff
            }

            FileStream stream = File.Open(fileName, FileMode.Open);

            Response.WriteFile(fileName);
		}
	}
}