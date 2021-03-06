﻿using System;
using System.Collections.Generic;
using System.Linq;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class OrganizationRepository : BaseRepository, IOrganizationRepository
	{
		public IEnumerable<Organization> GetAll()
		{
			using (var db = new EDEntities())
			{
				var organizations = db.Organizations
					.OrderBy(o => o.Name)
					.ToList();
				return organizations;
			}
		}

		public Organization Get(int organizationID)
		{
			using (var db = new EDEntities())
			{
				var organization = db.Organizations.SingleOrDefault(o => o.ID == organizationID);
				return organization;
			}
		}

		public Organization Add(Organization organizationToAdd)
		{
			using (var db = new EDEntities())
			{
				if (string.IsNullOrWhiteSpace(organizationToAdd.CreatedBy))
				{
					organizationToAdd.CreatedBy = this.UnknownAppUser;
				}

				organizationToAdd.CreatedDate = DateTime.UtcNow;

				db.Organizations.Add(organizationToAdd);
				db.SaveChanges();

				return organizationToAdd;
			}
		}

		public Organization Update(Organization organizationToUpdate)
		{
			using (var db = new EDEntities())
			{
				var organization = db.Organizations.SingleOrDefault(o => o.ID == organizationToUpdate.ID);

				if (organization == null)
				{
					throw new NullReferenceException("Organization not found.");
				}

				organization.Name = organizationToUpdate.Name;
				organization.Description = organizationToUpdate.Description;
				organization.PrivateDescription = organizationToUpdate.PrivateDescription;
				organization.Logo = organizationToUpdate.Logo;
				organization.AcceptancePolicy = organizationToUpdate.AcceptancePolicy;
				organization.PreliminaryGatheringDays = organizationToUpdate.PreliminaryGatheringDays;
				organization.PreliminaryVoteCount = organizationToUpdate.PreliminaryVoteCount;
				organization.GatheringDays = organizationToUpdate.GatheringDays;
				organization.VoteCount = organizationToUpdate.VoteCount;

				organization.ModifiedBy = string.IsNullOrWhiteSpace(organizationToUpdate.ModifiedBy)
					? this.UnknownAppUser
					: organizationToUpdate.ModifiedBy;
				organization.ModifiedDate = DateTime.UtcNow;

				db.SaveChanges();

				return organization;
			}
		}

		public bool Delete(int organizationID)
		{
			using (var db = new EDEntities())
			{
				var organization = db.Organizations.SingleOrDefault(o => o.ID == organizationID);

				db.Organizations.Remove(organization);
				db.SaveChanges();

				return true;
			}
		}
	}
}