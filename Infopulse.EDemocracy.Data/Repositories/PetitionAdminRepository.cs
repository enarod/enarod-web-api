using System.Collections.Generic;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities.Search;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class PetitionAdminRepository : PetitionRepository, IPetitionAdminRepository
	{
		public IEnumerable<Petition> GetPetitionForAdmin(SearchParameters searchParameters)
		{
			var petitions = this.Get(searchParameters, true);
			return petitions;
		}

		public void AssignApprover(int userID, IEnumerable<long> petitionIDs)
		{
			throw new System.NotImplementedException();
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