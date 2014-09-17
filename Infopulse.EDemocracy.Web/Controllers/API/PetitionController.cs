using AutoMapper;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Email;
using Infopulse.EDemocracy.Email.Notifications;
using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.ClientEntities;
using Infopulse.EDemocracy.Model.Common;
using Infopulse.EDemocracy.Web.CORS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
	/// <summary>
	/// APIs for managing petitions.
	/// </summary>
	[CorsPolicyProvider]
	public class PetitionController : BaseApiController
	{
		private readonly IPetitionRepository petitionRepository;
		private readonly IPetitionVoteRepository petitionVoteRepository;
		private readonly IPetitionLevelRepository petitionLevelRepository;
		private readonly IEntityRepository entityRepository;
		private readonly IRegionRepository regionRepository;


		/// <summary>
		/// Default constructor (no DI yet).
		/// </summary>
		public PetitionController()
		{
			this.petitionRepository = new PetitionRepository();
			this.petitionVoteRepository = new PetitionVoteRepository();
			this.petitionLevelRepository = new PetitionLevelRepository();
			this.entityRepository = new EntityRepository();
			this.regionRepository = new RegionRepository();
		}


		#region Get petitions

		/// <summary>
		/// Get all petitions.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("api/petition")]
		public OperationResult<IEnumerable<Petition>> Get()
		{
			return this.petitionRepository.Get();
		}


		/// <summary>
		/// Get petition by id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("api/petition/{id}")]
		public OperationResult<Petition> Get(int id)
		{
			return this.petitionRepository.Get(id);
		}

		#endregion


		#region Petition search

		/// <summary>
		/// Search petition by specific word in peetition description or subject.
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("api/petition/search/{query}")]
		public OperationResult<IEnumerable<Petition>> Search(string query)
		{
			var searchResults = this.petitionRepository.Search(query);
			return searchResults;
		}


		/// <summary>
		/// Search petition by specific tag.
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("api/petition/tag/{tag}")]
		public OperationResult<IEnumerable<Petition>> KeyWordSearch(string tag)
		{
			var searchResult = this.petitionRepository.KeyWordSearch(tag);
			return searchResult;
		}


		#endregion


		#region Vote

		/// <summary>
		/// Vote for petition by digital signature.
		/// </summary>
		/// <param name="vote">Petition vote via digital signature tool.</param>
		/// <returns></returns>
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


		/// <summary>
		/// Vote for petition by email.
		/// </summary>
		/// <param name="vote">Petition vote via email.</param>
		/// <returns></returns>
		[HttpPost]
		[Route("api/petition/emailVote")]
		public OperationResult EmailVote(EmailVote vote)
		{
			OperationResult result;

			var emailVoteRequest = this.petitionVoteRepository.CreateEmailVoteRequest(vote);

			if (!emailVoteRequest.IsSuccess)
			{
				result = emailVoteRequest;
				return result;
			}

			var notification = new PetitionVoteNotification(emailVoteRequest.Data);
			var sendingResult = NotificationService.Send(notification);
			if (sendingResult.IsSuccess)
			{
				result = sendingResult;
			}
			else
			{
				result = OperationResult.Fail(
					-11,
					string.Format("Не вдалось відправити запит на підтвердження голосування на email {0}", vote.Email));
			}

			return result;
		}

		#endregion


		/// <summary>
		/// Create new petition.
		/// </summary>
		/// <param name="petition"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("api/petition")]
		public OperationResult<Petition> CreatePetition([FromBody]Petition petition)
		{
			OperationResult<Petition> result;

			if (petition == null) return OperationResult<Petition>.Fail(-1, "Unable to parse incoming petition info.");

			// HACK:
			if (petition.Category == null || string.IsNullOrWhiteSpace(petition.Category.Name))
			{
				petition.Category = new Entity()
									{
										Name = EntityDictionary.Petition.Category.Etc
									};
			}

			// HACK:
			if (petition.Level == null || petition.Level.ID == default(int))
			{
				petition.Level = new PetitionLevel()
								 {
									 ID = 3
								 };
			}

			// HACK:
			if (string.IsNullOrWhiteSpace(petition.AddressedTo))
			{
				petition.AddressedTo = string.Empty;
			}

			petition.Limit = int.Parse(ConfigurationManager.AppSettings["NewPetitionLimit"]);
			petition.CreatedBy = new People() { Login = ConfigurationManager.AppSettings["AnonymousUserName"] };

			// create petition:
			var createPetitionResult = this.petitionRepository.AddNewPetition(petition);
			if (!createPetitionResult.IsSuccess) return createPetitionResult;

			// add email vote record to DB:
			var emailVoteAdded = this.petitionVoteRepository.CreateEmailVoteRequest(new EmailVote { ID = createPetitionResult.Data.ID, Email = petition.Email });
			if (!emailVoteAdded.IsSuccess || emailVoteAdded.Data == null)
			{
				result = OperationResult<Petition>.CopyFrom(emailVoteAdded);
				return result;
			}

			// send creation confirmation notification:
			var notification = new PetitionCreatedNotification(
					createPetitionResult.Data,
					emailVoteAdded.Data.ConfirmUrl);
			var sentResult = NotificationService.Send(notification);

			if (sentResult.IsSuccess)
			{
				result = OperationResult<Petition>.Success(
					1,
					string.Format(
						"Для підтвердження створення петиції перейдіть за посиланням, надісланому вам на email {0}.",
						petition.Email),
					createPetitionResult.Data);
			}
			else
			{
				result = OperationResult<Petition>.Fail(
					-13,
					"Під час спроби відправити листа сталася помилка. Перевірте правильність вказаного вами email'а і спробуйте ще раз.");
				result.DebugMessage = sentResult.Message;
			}

			return result;
		}


		#region Clear votes

		//[HttpDelete]
		//[Route("api/petition")]
		//public OperationResult Delete()
		//{
		//	var result = this.petitionVoteRepository.ClearVotes();
		//	return result;
		//}


		/// <summary>
		/// Clear votes for specific petition. [For testing purposes only!]
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("api/petition/{id}")]
		public OperationResult Delete([FromUri] int id)
		{
			var result = this.petitionVoteRepository.ClearVote(id);
			return result;
		}

		#endregion


		/// <summary>
		/// Get all petition levels.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("api/petition/level")]
		public OperationResult<IEnumerable<PetitionLevel>> GetPetitionLevels()
		{
			OperationResult<IEnumerable<PetitionLevel>> result;

			try
			{
				var petitionLevels = this.petitionLevelRepository.GetPetitionLevels();
				var clientPetitionLevels = Mapper.Map<IEnumerable<PetitionLevel>>(petitionLevels);
				result = OperationResult<IEnumerable<PetitionLevel>>.Success(clientPetitionLevels);
			}
			catch (Exception exc)
			{
				result = OperationResult<IEnumerable<PetitionLevel>>.ExceptionResult(exc);
			}

			return result;

			return result;
		}


		/// <summary>
		/// Get all petition categories.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("api/petition/category")]
		public OperationResult<IEnumerable<Entity>> GetPetitionCategories()
		{
			var cateroties = this.entityRepository.GetPetitionCategories();
			return cateroties;
		}


		/// <summary>
		/// Get all regions for specific petition level.
		/// </summary>
		/// <param name="petitionLevelID"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("api/petition/region/{petitionLevelID}")]
		public OperationResult<IEnumerable<Region>> GetPetitionRegions(int petitionLevelID)
		{
			var regions = this.regionRepository.GetRegions(petitionLevelID);
			return regions;
		}
	}
}