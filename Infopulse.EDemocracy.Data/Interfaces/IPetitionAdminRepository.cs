using System.Collections.Generic;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities.Search;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionAdminRepository : IPetitionRepository
	{
		IEnumerable<Petition> GetPetitionForAdmin(SearchParameters searchParameters);

		void AssignApprover(int userID, IEnumerable<long> petitionIDs);

		void ApprovePetitions(int userID, IEnumerable<long> petitionIDs);

		void RejectPetitions(int userID, IEnumerable<long> petitionIDs);

		void EscalatePetitions(int userID, IEnumerable<long> petitionIDs);
	}
}