﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using WebModels = Infopulse.EDemocracy.Model.BusinessEntities;
using DalModels = Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities.Search;
using Infopulse.EDemocracy.Web.Areas.Admin.Models;

namespace Infopulse.EDemocracy.Web.Areas.Admin.Controllers.API
{
    public class PetitionsController : ApiController
    {
		private IPetitionAdminRepository petitionAdminRepository = new PetitionAdminRepository();


		[Authorize]
		[HttpGet]
		[Route("api/admin/petitions")]
		public OperationResult<IEnumerable<WebModels.Petition>> Get(SearchParameters pageParameters)
	    {
		    var result = OperationExecuter.Execute(() =>
		    {
			    var petitions = petitionAdminRepository.Get(pageParameters ?? new SearchParameters(), true);
			    var webPetitions = petitions.Select(Mapper.Map<DalModels.Petition, WebModels.Petition>);
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
        public string AssignToMe([FromBody]IEnumerable<ModeratedPetition> petitions)
		{
			petitionAdminRepository = new PetitionAdminRepository();

			if (petitions.Any())
			{
				this.petitionAdminRepository.AssignApprover(-1, petitions.Select(p => p.ID));
			}

			return "success";
		}
	}
}