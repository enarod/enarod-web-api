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


		public PetitionController()
		{
			this.petitionRepository = new PetitionRepository();
			this.petitionVoteRepository = new PetitionVoteRepository();
		}


		public PetitionController(IPetitionRepository petitionRepository, IPetitionVoteRepository petitionVoteRepository)
		{
			this.petitionRepository = petitionRepository;
			this.petitionVoteRepository = petitionVoteRepository;
		}


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


		//[HttpPost]
		//public OperationResult Post(ClientPetitionVote vote)
		//{
		//	OperationResult result;

		//	try
		//	{
		//		if (vote.PetitionID == 0)
		//		{
		//			result = OperationResult.Fail(-2, "AgreementID was not provided.");
		//			return result;
		//		}

		//		// verify:
		//		IVerificationRepository verificationRepository;
		//		switch (vote.CertificateType)
		//		{
		//			case EntityDictionary.Certificate.Type.DPA:
		//				{
		//					verificationRepository = new DpaVerificationRepository();
		//					break;
		//				}
		//			case EntityDictionary.Certificate.Type.UACrypto:
		//				{
		//					verificationRepository = new UaCryptoVerificationRepository();
		//					break;
		//				}
		//			default:
		//				{
		//					verificationRepository = new UaCryptoVerificationRepository();
		//					break;
		//				}
		//		}

		//		var verificationResult = verificationRepository.Verify(vote.SignedData);
		//		var isVerficationSuccessfull =
		//			verificationResult.Descendants("Result").SingleOrDefault() != null &&
		//			verificationResult.Descendants("Result").SingleOrDefault().Value == "Success" &&
		//			verificationResult.Descendants("Serial").SingleOrDefault() != null &&
		//			verificationResult.Descendants("Serial").SingleOrDefault().Value.Length > 0;

		//		if (!isVerficationSuccessfull)
		//		{
		//			result = OperationResult.Fail(-3, "Certificate verification failed.");
		//			return result;
		//		}

		//		result = this.petitionVoteRepository.Vote(vote, verificationResult.Descendants("Serial").SingleOrDefault().Value);
		//	}
		//	catch (Exception exc)
		//	{
		//		result = OperationResult.ExceptionResult(exc);
		//	}

		//	return result;
		//}


		[HttpPost]
		public OperationResult Post(EmailVote vote)
		{
			var result = this.petitionVoteRepository.EmailVote(vote);
			return result;
		}


		public OperationResult Put(Petition petition)
		{
			return this.petitionRepository.AddNewPetition(petition);
		}


		[HttpDelete]
		public OperationResult Delete()
		{
			var result = this.petitionVoteRepository.ClearVotes();
			return result;
		}


		[HttpDelete]
		public OperationResult Delete([FromUri] int id)
		{
			var result = this.petitionVoteRepository.ClearVote(id);
			return result;
		}
	}
}