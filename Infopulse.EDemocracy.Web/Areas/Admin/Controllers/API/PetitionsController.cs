using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Model.BusinessEntities.Admin;
using WebModels = Infopulse.EDemocracy.Model.BusinessEntities;
using DalModels = Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities.Search;
using Infopulse.EDemocracy.Model.Enum;
using Infopulse.EDemocracy.Web.Auth;
using Infopulse.EDemocracy.Web.Controllers.API;

namespace Infopulse.EDemocracy.Web.Areas.Admin.Controllers.API
{
	public class PetitionsController : BaseApiController
	{
		private IPetitionAdminRepository petitionAdminRepository = new PetitionAdminRepository();


		[Authorize]
		[HttpGet]
		[Route("api/admin/petitions")]
		[AuthorizationCheckFilter(RequiredRoles = new[] { Role.Admin, Role.Moderator })]
		public OperationResult<IEnumerable<WebModels.Petition>> Get(SearchParameters pageParameters)
		{
			var result = OperationExecuter.Execute(() =>
			{
				var petitions = petitionAdminRepository.GetPetitionForAdmin(pageParameters);
				var webPetitions = petitions.Select(Mapper.Map<DalModels.Petition, WebModels.Admin.ModeratedPetition>);
				return OperationResult<IEnumerable<WebModels.Petition>>.Success(webPetitions);
			});

			return result;
		}

		/// <summary>
		/// Assigns current user as moderator for specific petitions.
		/// </summary>
		/// <param name="petitions">List of petitions to assign.</param>
		/// <returns>Operation result.</returns>
		[HttpPost]
		[AuthorizationCheckFilter(RequiredRoles = new[] { Role.Moderator })]
		[Route("api/admin/petitions/AssignToMe")]
		public OperationResult AssignToMe([FromBody]IEnumerable<ModeratedPetition> petitions)
		{
			if (!petitions.Any()) return OperationResult.Success(1, "No petitions ID was received.");
			this.petitionAdminRepository = new PetitionAdminRepository();

			var result = OperationExecuter.Execute(() =>
			{
				this.petitionAdminRepository.AssignApprover(this.GetSignedInUserId(), petitions.Select(p => p.ID));

				return OperationResult.Success();
			});

			return result;
		}


		/// <summary>
		/// Approve specific petitions for votes gathering.
		/// </summary>
		/// <param name="petitions">Petitions to approve.</param>
		/// <returns>Opertation result.</returns>
		[HttpPost]
		[AuthorizationCheckFilter(RequiredRoles = new[] { Role.Moderator })]
		[Route("api/admin/petitions/approve")]
		public OperationResult ApprovePetitions([FromBody]IEnumerable<ModeratedPetition> petitions)
		{
			if (!petitions.Any()) return OperationResult.Success(1, "No petitions ID was received.");
			this.petitionAdminRepository = new PetitionAdminRepository();

			var result = OperationExecuter.Execute(() =>
			{
				this.petitionAdminRepository.ApprovePetitions(this.GetSignedInUserId(), petitions.Select(p => p.ID));

				return OperationResult.Success();
			});

			return result;
		}

		/// <summary>
		/// Rejects specific petitions to show for all users.
		/// </summary>
		/// <param name="petitions">List of petitions to reject.</param>
		/// <returns>Operation result.</returns>
		[HttpPost]
		[AuthorizationCheckFilter(RequiredRoles = new[] { Role.Moderator })]
		[Route("api/admin/petitions/reject")]
		public OperationResult RejectPetitions([FromBody]IEnumerable<ModeratedPetition> petitions)
		{
			if (!petitions.Any()) return OperationResult.Success(1, "No petitions ID was received.");
			this.petitionAdminRepository = new PetitionAdminRepository();

			var result = OperationExecuter.Execute(() =>
			{
				this.petitionAdminRepository.RejectPetitions(this.GetSignedInUserId(), petitions.Select(p => p.ID));

				return OperationResult.Success();
			});

			return result;
		}

		/// <summary>
		/// Marks specific petitions as under consideration.
		/// </summary>
		/// <param name="petitions">Petitions list to escalate.</param>
		/// <returns>Operation result.</returns>
		[HttpPost]
		[AuthorizationCheckFilter(RequiredRoles = new[] { Role.Moderator })]
		[Route("api/admin/petitions/escalate")]
		public OperationResult EscalatePetitions([FromBody]IEnumerable<ModeratedPetition> petitions)
		{
			if (!petitions.Any()) return OperationResult.Success(1, "No petitions ID was received.");
			this.petitionAdminRepository = new PetitionAdminRepository();

			var result = OperationExecuter.Execute(() =>
			{
				this.petitionAdminRepository.EscalatePetitions(this.GetSignedInUserId(), petitions.Select(p => p.ID));

				return OperationResult.Success();
			});

			return result;
		}
	}
}
