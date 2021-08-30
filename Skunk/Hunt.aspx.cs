
using Puma.Prey.Rabbit.Context;

namespace Skunk
{
    public partial class Hunt : System.Web.UI.Page
    {
        private RabbitDBContext _db = new RabbitDBContext();
        /*
		protected void Page_Load(object sender, EventArgs e)
		{
			var hunt = (from p in _db.Hunts
						where p.Id == Convert.ToInt32(Request.QueryString["h"])
						select p).SingleOrDefault();
                       

			if (hunt != null)
			{
				lblProductName.Text = hunt.Name;
				lblPrice.Text = hunt.Price.ToString("C");
				lDetails.Text = Encoder.HtmlEncode(hunt.Description);
				hlRate.NavigateUrl = "~/Hunts/Feedback?p=" + hunt.Id.ToString();
				hProdID.Value = hunt.Id.ToString();
			}
		}
        */
    }
}