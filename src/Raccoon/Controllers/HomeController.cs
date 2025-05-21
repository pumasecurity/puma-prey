using Puma.Prey.Common.Deserialize;
using Puma.Prey.Raccoon.Models;
using System.Text;
using System.Web.Mvc;

namespace Puma.Prey.Raccoon.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactModel model)
        {
            ApplicationUser user = BinaryDeserialize.GetObject<ApplicationUser>(Encoding.UTF8.GetBytes(Request.Headers["User"]));

            return View();
        }
    }
}