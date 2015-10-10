using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Model.ClientEntities.Search;
using Infopulse.EDemocracy.Web.Areas.Admin.Models;
using DALModels = Infopulse.EDemocracy.Model;
using WebModels = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller to contain administration actions regarding petitions.
    /// </summary>
    public class PetitionsController : Controller
    {
		private IPetitionAdminRepository petitionAdminRepository = new PetitionAdminRepository();

		//private PetitionsController()
		//{
		//	this.petitionRepository = new PetitionRepository();
		//}


		//private PetitionsController(IPetitionRepository petitionRepository)
		//{
		//	this.petitionRepository = petitionRepository;
		//}


		public ActionResult Index()
		{
			petitionAdminRepository = new PetitionAdminRepository();
			var petitions = this.petitionAdminRepository.GetPetitionForAdmin(new SearchParameters());
			var webPetitions = petitions.Select(Mapper.Map<ModeratedPetition>).ToList();
			return View(webPetitions);
			//return View();
		}


		[HttpPost]
	    public ActionResult AssignToMe(IEnumerable<ModeratedPetition> petitions)
	    {
			if (petitions.Any())
			{
				this.petitionAdminRepository.AssignApprover(-1, GetSelectedPetitions(petitions));
			}
			
		    return this.RedirectToAction("Index");
	    }

		[HttpPost]
	    public ActionResult ApprovePetitions(List<ModeratedPetition> petitions)
	    {
			this.petitionAdminRepository.ApprovePetitions(-1, GetSelectedPetitions(petitions));
			return this.RedirectToAction("Index");
		}

		[HttpPost]
	    public ActionResult RejectPetitions(List<ModeratedPetition> petitions)
	    {
			this.petitionAdminRepository.RejectPetitions(-1, GetSelectedPetitions(petitions));
			return this.RedirectToAction("Index");
		}

		[HttpPost]
	    public ActionResult EscalatePetitions(List<ModeratedPetition> petitions)
	    {
			this.petitionAdminRepository.EscalatePetitions(-1, GetSelectedPetitions(petitions));
			return this.RedirectToAction("Index");
		}

	    private IEnumerable<long> GetSelectedPetitions(IEnumerable<ModeratedPetition> petitions)
	    {
		    return petitions.Where(p => p.IsChecked).Select(p => p.ID);
		}
	}
}