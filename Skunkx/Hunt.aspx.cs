using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.Security.Application;

using Puma.Prey.Rabbit.EF;

namespace Puma.Prey.Skunk
{
	public partial class Hunt : System.Web.UI.Page
	{
		private RabbitDBContext _db = new RabbitDBContext();

		protected void Page_Load(object sender, EventArgs e)
		{
			var hunt = (from p in _db.Hunts
							   where p.Id == Convert.ToInt32(Request.QueryString["p"])
							   select p).SingleOrDefault();

			lblProductName.Text = hunt.Name;
			lblPrice.Text = hunt.Price.ToString("C");
			lDetails.Text = Encoder.HtmlEncode(hunt.Description);
			hlRate.NavigateUrl = "~/Hunts/Feedback?p=" + hunt.Id.ToString();
			hProdID.Value = hunt.Id.ToString();
		}
	}
}