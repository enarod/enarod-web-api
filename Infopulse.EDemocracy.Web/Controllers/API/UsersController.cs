using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Web.CORS;

using DALModels = Infopulse.EDemocracy.Model;
using WebModels = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
	[CorsPolicyProvider]
	public class UsersController : BaseApiController
    {
	    //private readonly IUserDetailRepository userDetailRepository;
		private readonly IPetitionRepository petitionRepository;

		public UsersController() : base()
	    {
		    //this.userDetailRepository = new UserDetailRepository();
			this.petitionRepository = new PetitionRepository();
	    }

	    public UsersController(IUserDetailRepository userDetailsRepository, IPetitionRepository petitionRepository)
	    {
		    this.userDetailRepository = userDetailsRepository;
		    this.petitionRepository = petitionRepository;
	    }

		/// <summary>
		/// Get UserDetailInfo about current user.
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		[Route("api/users/current")]
		public OperationResult<WebModels.UserDetailInfo> Get(bool showCreated = false, bool showSigned = false)
		{
			var result = OperationExecuter.Execute<WebModels.UserDetailInfo>(() =>
			{
				var currentUserID = this.GetSignedInUserId();
				var userInfo = this.userDetailRepository.Get(currentUserID);
				var userInfoWeb = Mapper.Map<DALModels.UserDetail, WebModels.UserDetailInfo>(userInfo);
				if (showCreated)
				{
					userInfoWeb.CreatedPetitions = this.petitionRepository.GetPetitionsCreatedByUser(currentUserID)
						.Select(Mapper.Map<DALModels.Petition, WebModels.Petition>)
						.ToList();
				}

				if (showSigned)
				{
					userInfoWeb.SignedPetitions = this.petitionRepository.GetPetitionsSignedByUser(currentUserID)
						.Select(Mapper.Map<DALModels.Petition, WebModels.Petition>)
						.ToList();
				}
				
				return OperationResult<WebModels.UserDetailInfo>.Success(userInfoWeb);
			});

			return result;
		}

		/// <summary>
		/// Saves changes in current user's details.
		/// </summary>
		/// <param name="userDetailInfo">Data to save.</param>
		/// <returns>Operation result of db update.</returns>
		[Authorize]
		[HttpPut]
		[Route("api/users/current")]
		public OperationResult Put(WebModels.UserDetailInfo userDetailInfo)
		{
			var result = OperationExecuter.Execute(() =>
			{
				var currentUserID = this.GetSignedInUserId();

				var dalUserDetailInfo = Mapper.Map<WebModels.UserDetailInfo, DALModels.UserDetail>(userDetailInfo);
				dalUserDetailInfo.UserID = currentUserID;
                var updatedInfo = this.userDetailRepository.Update(dalUserDetailInfo);
				return OperationResult.Success(1, "Ваші дані успішно збережені.");
			});
			return result;
		}
    }
}