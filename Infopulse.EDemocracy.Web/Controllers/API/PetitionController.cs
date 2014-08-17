using System.Configuration;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.ClientEntities;
using Infopulse.EDemocracy.Model.Common;
using Infopulse.EDemocracy.Web.CORS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
	[CorsPolicyProvider]
	public class PetitionController : BaseApiController
	{
		private readonly IPetitionRepository petitionRepository;
		private readonly IPetitionVoteRepository petitionVoteRepository;
		private readonly IPetitionLevelRepository petitionLevelRepository;
		private readonly IEntityRepository entityRepository;


		public PetitionController()
		{
			this.petitionRepository = new PetitionRepository();
			this.petitionVoteRepository = new PetitionVoteRepository();
			this.petitionLevelRepository = new PetitionLevelRepository();
			this.entityRepository = new EntityRepository();
		}


		#region Get petitions

		public OperationResult<IEnumerable<Petition>> Get()
		{
			return this.petitionRepository.Get();
		}


		public OperationResult<Petition> Get(int id)
		{
			return this.petitionRepository.Get(id);
		}

		
		[HttpGet]
		[Route("api/petition/search/{query}")]
		public OperationResult<IEnumerable<Petition>> Search(string query)
		{
			var searchResults = this.petitionRepository.Search(query);
			return searchResults;
		}

		#endregion


		#region Vote

		[HttpPost]
		[Route("api/petition/digitalSignatureVote")]
		public OperationResult DigitalSignatureVote(ClientPetitionVote vote)
		{
			OperationResult result;

			try
			{
				if (vote.PetitionID == 0)
				{
					result = OperationResult.Fail(-2, "PetitionID was not provided.");
					return result;
				}

				// verify:
				IVerificationRepository verificationRepository;
				switch (vote.CertificateType)
				{
					case EntityDictionary.Certificate.Type.DPA:
						{
							verificationRepository = new DpaVerificationRepository();
							break;
						}
					case EntityDictionary.Certificate.Type.UACrypto:
						{
							verificationRepository = new UaCryptoVerificationRepository();
							break;
						}
					default:
						{
							verificationRepository = new UaCryptoVerificationRepository();
							break;
						}
				}

				var verificationResult = verificationRepository.Verify(vote.SignedData);
				var isVerficationSuccessfull =
					verificationResult.Descendants("Result").SingleOrDefault() != null &&
					verificationResult.Descendants("Result").SingleOrDefault().Value == "Success" &&
					verificationResult.Descendants("Serial").SingleOrDefault() != null &&
					verificationResult.Descendants("Serial").SingleOrDefault().Value.Length > 0;

				if (!isVerficationSuccessfull)
				{
					result = OperationResult.Fail(-3, "Certificate verification failed.");
					return result;
				}

				result = this.petitionVoteRepository.Vote(vote, verificationResult.Descendants("Serial").SingleOrDefault().Value);
			}
			catch (Exception exc)
			{
				result = OperationResult.ExceptionResult(exc);
			}

			return result;
		}


		[HttpPost]
		[Route("api/petition")]
		public OperationResult EmailVote(EmailVote vote)
		{
			var result = this.petitionVoteRepository.EmailVote(vote);
			return result;
		}

		#endregion


		public OperationResult Put(Petition petition)
		{
			petition.Limit = int.Parse(ConfigurationManager.AppSettings["NewPetitionLimit"]);
			petition.CreatedBy = new People() { Login = ConfigurationManager.AppSettings["AnonymousUserName"] };
			return this.petitionRepository.AddNewPetition(petition);
		}


		#region Clear votes

		//[HttpDelete]
		//public OperationResult Delete()
		//{
		//	var result = this.petitionVoteRepository.ClearVotes();
		//	return result;
		//}


		[HttpDelete]
		public OperationResult Delete([FromUri] int id)
		{
			var result = this.petitionVoteRepository.ClearVote(id);
			return result;
		}

		#endregion


		[HttpGet]
		[Route("api/petition/level")]
		public OperationResult<IEnumerable<PetitionLevel>> GetPetitionLevels()
		{
			var result = this.petitionLevelRepository.GetPetitionLevels();
			return result;
		}


		[HttpGet]
		[Route("api/petition/category")]
		public OperationResult<IEnumerable<Entity>> GetPetitionCategories()
		{
			var cateroties = this.entityRepository.GetPetitionCategories();
			return cateroties;
		}
	}
}