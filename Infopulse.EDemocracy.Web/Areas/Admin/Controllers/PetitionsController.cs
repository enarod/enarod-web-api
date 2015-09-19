using System.Web.Mvc;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Model.ClientEntities.Search;

namespace Infopulse.EDemocracy.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller to contain administration actions regarding petitions.
    /// </summary>
    public class PetitionsController : Controller
    {
	    private readonly IPetitionRepository petitionRepository;

	    private PetitionsController()
	    {
		    this.petitionRepository = new PetitionRepository();
	    }

        // GET: Admin/Petitions
        public ActionResult Index()
        {
	        var petitions = this.petitionRepository.GetPetitionForAdmin(new SearchParameters());
            return View();
        }
    }
}