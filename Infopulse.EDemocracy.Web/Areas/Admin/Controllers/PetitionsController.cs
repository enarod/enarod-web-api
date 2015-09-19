using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Model.ClientEntities.Search;

namespace Infopulse.EDemocracy.Web.Areas.Admin.Controllers
{
    public class PetitionsController : Controller
    {
	    private IPetitionRepository petitionRepository;

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