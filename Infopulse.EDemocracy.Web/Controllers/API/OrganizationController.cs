﻿using AutoMapper;
using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Web.CORS;
using System.Collections.Generic;
using System.Web.Http;
using DALModels = Infopulse.EDemocracy.Model;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
	/// <summary>
	/// Controller to operate with Organizrion entity.
	/// </summary>
	[CorsPolicyProvider]
	public class OrganizationController : BaseApiController
    {
	    private readonly IOrganizationRepository organizationRepository;

		/// <summary>
		/// Default constructor.
		/// </summary>
	    public OrganizationController()
	    {
		    this.organizationRepository = new OrganizationRepository();
	    }


		/// <summary>
		/// Gets all organization registered in DB.
		/// </summary>
		/// <returns>List of organizations.</returns>
		[HttpGet]
		[Route("api/Organizations/all")]
		public OperationResult<IEnumerable<Organization>> Get()
		{
			var result = OperationExecuter.Execute(() =>
			{
				var organizations = this.organizationRepository.GetAll();
				var webOrganizations = Mapper.Map<IEnumerable<Organization>>(organizations);
				return OperationResult<IEnumerable<Organization>>.Success(webOrganizations);
			});

			return result;
		}


		/// <summary>
		/// Gets all organization registered in DB.
		/// </summary>
		/// <returns>List of organizations.</returns>
		[HttpGet]
		[Route("api/Organizations/{id:int}")]
		public OperationResult<Organization> Get(int id)
		{
			var result = OperationExecuter.Execute(() =>
			{
				var organization = this.organizationRepository.Get(id);
				var webOrganization = Mapper.Map<Organization>(organization);
				return OperationResult<Organization>.Success(webOrganization);
			});

			return result;
		}


		/// <summary>
		/// Adds new organization.
		/// </summary>
		/// <param name="organizationToAdd">New organization.</param>
		/// <returns>Organization been added.</returns>
		public OperationResult<Organization> Post([FromBody]Organization organizationToAdd)
		{
			var result = OperationExecuter.Execute(() =>
			{
				var dbOrganization = Mapper.Map<DALModels.Organization>(organizationToAdd);
				var organization = this.organizationRepository.Add(dbOrganization);
				var webOrganization = Mapper.Map<Organization>(organization);
				return OperationResult<Organization>.Success(webOrganization);
			});

			return result;
		}
    }
}