using System.Web.Mvc;

namespace Infopulse.EDemocracy.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller to contain administration actions regarding petitions.
    /// </summary>
    public class PetitionsController : Controller
    {
		public ActionResult Index()
		{
			return View();
		}
	}
}