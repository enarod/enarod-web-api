using Infopulse.EDemocracy.Common.Exceptions;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace Infopulse.EDemocracy.Web.Controllers
{
	/// <summary>
	/// MVC controller for all petition pages.
	/// </summary>
	public class PetitionController : Controller
	{
		private readonly IPetitionVoteRepository petitionVoteRepository;


		public PetitionController()
		{
			this.petitionVoteRepository = new PetitionVoteRepository();
		}


		public PetitionController(IPetitionVoteRepository petitionVoteRepository)
		{
			this.petitionVoteRepository = petitionVoteRepository;
		}


		public ActionResult Index()
		{
			return View();
		}


		public ActionResult Vote(string hash)
		{
			const string redirectUrl = "{0}/petition/#petition/{1}/{2}";
			long petitionID = -1;
			string actionResult;

			try
			{
				var confirmedPetitionVote = this.petitionVoteRepository.ConfirmEmailVoteRequest(hash);
				
				if (confirmedPetitionVote != null && confirmedPetitionVote.IsConfirmed)
				{
					petitionID = confirmedPetitionVote.PetitionID;
				}

				actionResult = "voteConfirmed";
			}
			catch (PetitionAlreadyVotedWithEmailException exc)
			{
				actionResult = "alreadyVoted";
			}
			catch (Exception exc)
			{
				actionResult = "error";
			}

			return this.Redirect(string.Format(
				redirectUrl,
				ConfigurationManager.AppSettings["AppDomain"],
				petitionID,
				actionResult));
		}
	}
}