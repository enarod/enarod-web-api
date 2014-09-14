using System.Web.Mvc;

namespace Infopulse.EDemocracy.Web.Controllers
{
	public class AdminController : Controller
	{
		public ActionResult Index()
		{
			return this.RedirectToAction("index", "home", new { area = "admin" });
		}
	}
}