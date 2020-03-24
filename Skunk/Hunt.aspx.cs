using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.CodeAnalysis;
using Microsoft.Security.Application;

using Puma.Prey.Rabbit.EF;

namespace Skunk
{
	public partial class Hunt : System.Web.UI.Page
	{
		private RabbitDBContext _db = new RabbitDBContext();

		protected void Page_Load(object sender, EventArgs e)
		{
			var hunt = (from p in _db.Hunts
						where p.Id == Convert.ToInt32(Request.QueryString["h"])
						select p).SingleOrDefault();

			if (hunt != null)
			{
				lblProductName.Text = Encoder.HtmlEncode(hunt.Name);
				lblPrice.Text = hunt.Price.ToString("C");

				hlRate.NavigateUrl = "~/Hunts/Feedback?p=" + hunt.Id.ToString();
				hProdID.Value = hunt.Id.ToString();
			}

			var untrustedClass = new Puma.Prey.Common.UntrustedLibrary.UntrustedClass();
			lDetails.Text = untrustedClass.GetDangerousValue(); 
		}
	}
}