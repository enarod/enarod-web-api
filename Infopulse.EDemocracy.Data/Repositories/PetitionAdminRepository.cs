using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities.Search;
using Infopulse.EDemocracy.Model.Enum;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class PetitionAdminRepository : PetitionRepository, IPetitionAdminRepository
	{
		public IEnumerable<Petition> GetPetitionForAdmin(SearchParameters searchParameters)
		{
			if (searchParameters == null) searchParameters = new SearchParameters();
			searchParameters.SetEmptyValues();
			var orderBy = $"{searchParameters.OrderBy} {searchParameters.OrderDirection}";

			//var petitions = this.Get(searchParameters, true);
			using (var db = new EDEntities())
			{
				var petitions = db.Petitions
					.Include(p => p.Approver)
					.Include(p => p.Approver.UserDetails)
					.Include(p => p.Approver.UserDetails.Select(ud => ud.User))
					.Include(p => p.Author)
					.Include(p => p.Author.UserDetails)
					.Include(p => p.Organization)
					.Include(p => p.Category)
					.Include(p => p.Category.EntityGroup)
					.Include(p => p.PetitionLevel)
					.Include(p => p.PetitionStatus);

				if (orderBy.Equals("ID ASC", StringComparison.InvariantCultureIgnoreCase))
					petitions = petitions.OrderBy(p => p.ID);
				else if (orderBy.Equals("ID DESC", StringComparison.InvariantCultureIgnoreCase))
					petitions = petitions.OrderByDescending(p => p.ID);

				else if (orderBy.Equals("CreatedDate ASC", StringComparison.InvariantCultureIgnoreCase))
					petitions = petitions.OrderBy(p => p.CreatedDate);
				else if (orderBy.Equals("CreatedDate DESC", StringComparison.InvariantCultureIgnoreCase))
					petitions = petitions.OrderByDescending(p => p.CreatedDate);

				else if (orderBy.Equals("Approver ASC", StringComparison.InvariantCultureIgnoreCase))
					petitions = petitions.OrderBy(p => p.Approver.Email);
				else if (orderBy.Equals("Approver DESC", StringComparison.InvariantCultureIgnoreCase))
					petitions = petitions.OrderByDescending(p => p.Approver.Email);

				return petitions
					.Skip(searchParameters.PageSize.Value * (searchParameters.PageNumber.Value - 1))
					.Take(searchParameters.PageSize.Value)
					.ToList();
			}
		}

		public void AssignApprover(int userID, IEnumerable<long> petitionIDs)
		{
			using (var db = new EDEntities())
			{
				var petitions = db.Petitions.Where(p => petitionIDs.Contains(p.ID));
				foreach (var petition in petitions)
				{
					petition.ApprovedBy = userID;
					petition.PetitionStatusID = (int)PetitionStatusEnum.Moderation;
				}

				db.SaveChanges();
			}
		}

		public void ApprovePetitions(int userID, IEnumerable<long> petitionIDs)
		{
			throw new System.NotImplementedException();
		}

		public void RejectPetitions(int userID, IEnumerable<long> petitionIDs)
		{
			throw new System.NotImplementedException();
		}

		public void EscalatePetitions(int userID, IEnumerable<long> petitionIDs)
		{
			throw new System.NotImplementedException();
		}
	}
}