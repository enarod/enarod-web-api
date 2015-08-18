using AutoMapper;
using Infopulse.EDemocracy.Common.Cache;
using Infopulse.EDemocracy.Common.Cache.Interfaces;
using Infopulse.EDemocracy.Common.Exceptions;
using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Common.Services;
using Infopulse.EDemocracy.Common.Services.Models;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Email;
using Infopulse.EDemocracy.Email.Notifications;
using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.ClientEntities;
using Infopulse.EDemocracy.Model.ClientEntities.Search;
using Infopulse.EDemocracy.Model.Common;
using Infopulse.EDemocracy.Web.CORS;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using DALModel = Infopulse.EDemocracy.Model;
using System;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
	/// <summary>
	/// APIs for managing petitions.
	/// </summary>
	[CorsPolicyProvider]
	public class PetitionController : BaseApiController
	{
		private readonly IPetitionRepository petitionRepository;
		private readonly IPetitionLevelRepository petitionLevelRepository;
		private readonly IEntityRepository entityRepository;
		private readonly IRegionRepository regionRepository;
		private readonly IDictionariesHelper dictionariesHelper;
		private readonly IEntityCache entityCache;

		private readonly GeoService geoService;

		private readonly EDemocracy.Data.Interfaces.v2.IPetitionVoteRepository petitionVoteRepository;

		/// <summary>
		/// Default constructor (no DI yet).
		/// </summary>
		public PetitionController() : base()
		{
			this.petitionRepository = new PetitionRepository();
			this.petitionLevelRepository = new PetitionLevelRepository();
			this.entityRepository = new EntityRepository();
			this.regionRepository = new RegionRepository();
			this.dictionariesHelper = new DictionariesHelper();
			this.entityCache = new EntityCache(new CacheProvider());

			this.geoService = new GeoService();

			this.petitionVoteRepository = new PetitionVoteRepository();
		}


		#region Get petitions

		/// <summary>
		/// Get all petitions.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("api/petition")]
		public OperationResult<IEnumerable<Petition>> GetAll(SearchParameters searchParameters, bool showInactivePetitions = false)
		{
			var result = OperationExecuter.Execute(() =>
			{
				if (searchParameters == null)
				{
					searchParameters = SearchParameters.Default;
				}

				var petitions = this.petitionRepository.Get(searchParameters, showInactivePetitions);
				
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
		[Route("api/petition/{id:int}")]
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
		/// <param name="searchParameters">All search attributes.</param>
		/// <returns></returns>
		[HttpGet]
		[Route("api/petition/search")]
		public OperationResult<IEnumerable<Petition>> Search([FromUri] PetitionSearchParameters searchParameters)
		{
			if (searchParameters == null)
			{
				return this.GetAll(SearchParameters.Default);
			}

			var result = OperationExecuter.Execute(() =>
			{
				var petitions = this.petitionRepository.Search(searchParameters).ToList();
				this.SetDictionariesValues(petitions);
				var clientPetitions = petitions.Select(Mapper.Map<DALModel.PetitionWithVote, Petition>);
				return OperationResult<IEnumerable<Petition>>.Success(clientPetitions);
			});

			return result;
		}


		/// <summary>
		/// Search petition by specific tag.
		/// </summary>
		/// <param name="tag">Tag text.</param>
		/// <param name="searchParameters">Search parameters which includes keyword.</param>
		/// <param name="showActivePetitions"></param>
		/// <param name="showInactivePetitions"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("api/petition/tag")]
		public OperationResult<IEnumerable<Petition>> KeyWordSearch(
			string tag,
			[FromUri] SearchParameters searchParameters,
			bool showActivePetitions = true,
			bool showInactivePetitions = false)
		{
			var result = OperationExecuter.Execute(() =>
			{
				var keyWordSearchParameters = new PetitionSearchParameters()
				{
					KeyWord = tag,
					PageNumber = searchParameters.PageNumber,
					PageSize = searchParameters.PageSize,
					OrderBy = searchParameters.OrderBy,
					OrderDirection = searchParameters.OrderDirection,
					ShowActivePetitions = showActivePetitions,
					ShowInactivePetitions = showInactivePetitions
				};

				var petitions = this.petitionRepository.Search(keyWordSearchParameters);
				this.SetDictionariesValues(petitions);
				var clientPetitions = Mapper.Map<IEnumerable<Petition>>(petitions);
				return OperationResult<IEnumerable<Petition>>.Success(clientPetitions);
			});

			return result;
		}


		#endregion


		#region Vote

		/// <summary>
		/// Vote for petition by email.
		/// </summary>
		/// <param name="vote2">Information about petition signer.</param>
		/// <returns></returns>
		/// <remarks>Version 2</remarks>
		[HttpPost]
		[Route("api/petition/v2/emailVote")]
		public OperationResult EmailVote2(EmailVote vote)
		{
			var result = OperationExecuter.Execute(() =>
			{
				OperationResult voteResult;

				var emailVoteRequest = this.petitionVoteRepository.CreateEmailVoteRequest(
					Mapper.Map<Infopulse.EDemocracy.Model.PetitionEmailVote>(vote));

				var webPetitionVote = Mapper.Map<Infopulse.EDemocracy.Model.BusinessEntities.PetitionEmailVote>(emailVoteRequest);
				var notification = new PetitionVoteNotification(webPetitionVote);

				var sendingResult = NotificationService.Send(notification);
				if (sendingResult.IsSuccess)
				{
					voteResult = sendingResult;
				}
				else
				{
					voteResult = OperationResult.Fail(
						-11,
						string.Format("Не вдалось відправити запит на підтвердження голосування на email {0}", vote.Signer.Email));
				}

				return voteResult;
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
		[Authorize]
		[Route("api/petition")]
		public OperationResult<Petition> CreatePetition([FromBody]Petition petition)
		{
			return OperationExecuter.Execute(() =>
			{
				OperationResult<Petition> result = null;

				if (petition == null)
				{
					return OperationResult<Petition>.ExceptionResult(
						new UnableToReadPetitionException("Unalbe to parse incoming JSON."));
				}

				SetPetitionDefaultValues(petition);
				
				// create petition:
				var dalPetition = Mapper.Map<DALModel.Petition>(petition);
				var petitionAuthor = this.userDetailRepository.Update(dalPetition.CreatedByUser);
                dalPetition.CreatedByUser = petitionAuthor;
				dalPetition.CreatedBy = petitionAuthor.ID;
								
				dalPetition = this.petitionRepository.AddNewPetition(dalPetition);
				
				// add email vote record to DB:
				var dalEmailVoteAdded = this.petitionVoteRepository.CreateEmailVoteRequest(
					new DALModel.PetitionEmailVote()
					{
						PetitionID = dalPetition.ID,
						PetitionSigner = new Model.PetitionSigner()
						{
							Email = petition.Email
						}
					});
				var emailVoteAdded = Mapper.Map<Model.PetitionEmailVote, PetitionEmailVote>(dalEmailVoteAdded);
				
				// send creation confirmation notification:
				petition = Mapper.Map<Model.Petition, Petition>(dalPetition);
				var notification = new PetitionCreatedNotification(petition, emailVoteAdded.ConfirmUrl);
				var sentResult = NotificationService.Send(notification);

				if (sentResult.IsSuccess)
				{
					result = OperationResult<Petition>.Success(
						1,
						string.Format(
							"Для підтвердження створення петиції перейдіть за посиланням, надісланому вам на email {0}.",
							petition.Email),
						petition);
				}
				else
				{
					result = OperationResult<Petition>.Fail(
						-13,
						"Під час спроби відправити листа сталася помилка. Перевірте правильність вказаного вами email'а і спробуйте ще раз.");
					result.DebugMessage = sentResult.Message;
				}

				return result;
			});

			
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
			return OperationExecuter.Execute(() =>
			{
				this.petitionVoteRepository.ClearVote(id);
				return OperationResult.Success(1, string.Format("Голоса за петицію {0} було видалено.", id));
			});
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

		private void SetPetitionDefaultValues(Petition petition)
		{
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

			if (petition.CreatedBy == null)
			{
				petition.CreatedBy = new UserDetail();
            }

			petition.CreatedBy.UserID = this.GetSignedInUserId();
        }
	}
}