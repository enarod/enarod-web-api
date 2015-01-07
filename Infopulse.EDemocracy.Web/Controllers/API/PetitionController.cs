using System.Data.Entity;
using AutoMapper;
using Infopulse.EDemocracy.Common.Cache;
using Infopulse.EDemocracy.Common.Cache.Interfaces;
using Infopulse.EDemocracy.Common.Services;
using Infopulse.EDemocracy.Common.Services.Models;
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
using DALModel = Infopulse.EDemocracy.Model;

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
		private readonly IDictionariesHelper dictionariesHelper;
		private readonly IEntityCache entityCache;

		private readonly GeoService geoService;

		private readonly EDemocracy.Data.Interfaces.v2.IPetitionVoteRepository petitionVoteRepository2;

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
			this.dictionariesHelper = new DictionariesHelper();
			this.entityCache = new EntityCache(new CacheProvider());

			this.geoService = new GeoService();

			this.petitionVoteRepository2 = new EDemocracy.Data.Repositories.v2.PetitionVoteRepository();
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
			var result = OperationExecuter.Execute(() =>
			{
				var petitions = this.petitionRepository.Get();
				
				this.SetDictionariesValues(petitions);

				var clinetPetitions = Mapper.Map<IEnumerable<Petition>>(petitions);
				return OperationResult<IEnumerable<Petition>>.Success(clinetPetitions);
			});

			return result;
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
			var result = OperationExecuter.Execute(() =>
			{
				var petition = this.petitionRepository.Get(id);
				this.SetDictionariesValues(petition);
				var clientPetition = Mapper.Map<Petition>(petition);
				return OperationResult<Petition>.Success(clientPetition);
			});

			return result;
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
			var result = OperationExecuter.Execute(() =>
			{
				var petitions = this.petitionRepository.Search(query);
				var clientPetitions = Mapper.Map<IEnumerable<Petition>>(petitions);
				return OperationResult<IEnumerable<Petition>>.Success(clientPetitions);
			});

			return result;
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
			var result = OperationExecuter.Execute(() =>
			{
				var petitions = this.petitionRepository.KeyWordSearch(tag);
				var clientPetitions = Mapper.Map<IEnumerable<Petition>>(petitions);
				return OperationResult<IEnumerable<Petition>>.Success(clientPetitions);
			});

			return result;
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


		/// <summary>
		/// Vote for petition by email.
		/// </summary>
		/// <param name="vote">Information about petition signer.</param>
		/// <returns></returns>
		/// <remarks>Version 2</remarks>
		[HttpPost]
		[Route("api/petition/v2/emailVote")]
		public OperationResult EmailVote2(EDemocracy.Model.ClientEntities.v2.EmailVote vote)
		{
			var result = OperationExecuter.Execute(() =>
			{
				var dalEmailVote = Mapper.Map<EDemocracy.Model.PetitionEmailVote>(vote);
				var emailVoteRequest = this.petitionVoteRepository2.CreateEmailVoteRequest(dalEmailVote);
				return OperationResult.Success(1, "Success");
			});

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
			var createPetitionResult = OperationExecuter.Execute(() =>
			{
				var dalPetition = Mapper.Map<DALModel.Petition>(petition);
				dalPetition = this.petitionRepository.AddNewPetition(dalPetition);
				var createdClientPetition = Mapper.Map<Petition>(dalPetition);
				return OperationResult<Petition>.Success(createdClientPetition);
			});
			
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
			var result = OperationExecuter.Execute(() =>
			{
				var petitionLevels = this.petitionLevelRepository.GetPetitionLevels();
				var clientPetitionLevels = Mapper.Map<IEnumerable<PetitionLevel>>(petitionLevels);
				return OperationResult<IEnumerable<PetitionLevel>>.Success(clientPetitionLevels);
			});

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
			var result = OperationExecuter.Execute(() =>
			{
				var categories = this.entityRepository.GetPetitionCategories();
				var clientCategories = Mapper.Map<IEnumerable<Entity>>(categories);
				return OperationResult<IEnumerable<Entity>>.Success(clientCategories);
			});

			return result;
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
			// TODO: RegionRepo is not implemented yet. So this method should
			// be rewrited after RegionRepo will be rewritten.

			var regions = this.regionRepository.GetRegions(petitionLevelID);
			return regions;
		}


		[HttpGet]
		[Route("api/countries")]
		public OperationResult<IEnumerable<Country>> GetCountries()
		{
			var countries = this.entityCache.Get(CachedElement.CountriesList, () =>
			{
				var countriesList = this.geoService.GetCountries();
				return countriesList;
			}) as IEnumerable<Country>;

			return OperationResult<IEnumerable<Country>>.Success(countries);
		}


		private void SetDictionariesValues(IEnumerable<DALModel.Petition> petitions)
		{
			var levels = this.entityCache.Get(CachedElement.PetitionLevel, this.dictionariesHelper.GetPetitionLevels)
					as IEnumerable<DALModel.PetitionLevel>;
			var categories = this.entityCache.Get(CachedElement.PetitionCategory, this.dictionariesHelper.GetCategories)
				as IEnumerable<DALModel.Entity>;

			foreach (var petition in petitions)
			{
				petition.PetitionLevel = levels.SingleOrDefault(l => l.ID == petition.LevelID);
				petition.Category = categories.SingleOrDefault(c => c.ID == petition.CategoryID);
			}
		}

		private void SetDictionariesValues(params DALModel.Petition[] petitions)
		{
			this.SetDictionariesValues(petitions.ToList());
		}
	}
}