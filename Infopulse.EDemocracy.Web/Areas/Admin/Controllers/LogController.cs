using Infopulse.EDemocracy.Data;
using System.Web.Mvc;

namespace Infopulse.EDemocracy.Web.Areas.Admin.Controllers
{
    public class LogController : Controller
    {
        public ActionResult Index()
        {
	        var model = DbLog.Log;
            return View(model);
        }

	    public ActionResult Clear()
	    {
		    DbLog.Clear();
		    return this.RedirectToAction("Index");
	    }
	}
}