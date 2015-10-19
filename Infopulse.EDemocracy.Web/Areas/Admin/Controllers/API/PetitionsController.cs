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
using Infopulse.EDemocracy.Web.Controllers.API;

namespace Infopulse.EDemocracy.Web.Areas.Admin.Controllers.API
{
    public class PetitionsController : BaseApiController
    {
		private IPetitionAdminRepository petitionAdminRepository = new PetitionAdminRepository();


		[Authorize]
		[HttpGet]
		[Route("api/admin/petitions")]
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
		/// <returns></returns>
		[HttpPost]
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
	}
}
