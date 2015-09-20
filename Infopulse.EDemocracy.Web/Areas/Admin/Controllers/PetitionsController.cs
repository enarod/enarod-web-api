using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Model.ClientEntities.Search;

using DALModels = Infopulse.EDemocracy.Model;
using WebModels = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller to contain administration actions regarding petitions.
    /// </summary>
    public class PetitionsController : Controller
    {
		private IPetitionRepository petitionRepository = new PetitionRepository();

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
			petitionRepository = new PetitionRepository();
			var petitions = this.petitionRepository.GetPetitionForAdmin(new SearchParameters());
			var webPetitions = petitions.Select(Mapper.Map<WebModels.Petition>).ToList();
			return View(webPetitions);
			//return View();
		}
	}
}