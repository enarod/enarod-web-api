﻿using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using System.Web.Mvc;

namespace Infopulse.EDemocracy.Web.Controllers
{
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
			var redirectUrl = "https://enarod.org/petition/#petition/{0}/{1}";
			long petitionID = -1;
			var actionResult = string.Empty;


			var confirmedPetitionVoteResult = this.petitionVoteRepository.ConfirmPetitionEmailVote(hash);
			if (confirmedPetitionVoteResult.Data != null)
			{
				petitionID = confirmedPetitionVoteResult.Data.PetitionID;
			}

			switch (confirmedPetitionVoteResult.ResultCode)
			{
				case -2:
					{
						actionResult = "alreadyVoted";
						break;
					}
				case 1:
					{
						actionResult = "voteConfirmed";
						break;
					}
				default:
					{
						actionResult = "error";
						break;
					}
			}

			return this.Redirect(string.Format(redirectUrl, petitionID, actionResult));
		}
	}
}