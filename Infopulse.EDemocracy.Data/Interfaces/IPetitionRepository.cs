using Infopulse.EDemocracy.Model;
using System.Collections.Generic;
using Infopulse.EDemocracy.Model.ClientEntities.Search;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionRepository
	{
		/// <summary>
		/// Get petition by ID.
		/// </summary>
		PetitionWithVote Get(int petitionID);
		
		
		/// <summary>
		/// Get all petitions.
		/// </summary>
		IEnumerable<PetitionWithVote> Get(SearchParameters searchParameters, bool showPreliminaryPetitions = false);


		/// <summary>
		/// Create new petitions.
		/// </summary>
		Petition AddNewPetition(Petition newPetition);
        
		
		/// <summary>
		/// Searches among all petitions by requested search paramters.
		/// </summary>
		/// <param name="searchParameters">All search attributes.</param>
		IEnumerable<PetitionWithVote> Search(PetitionSearchParameters searchParameters);


		IEnumerable<Petition> GetPetitionForAdmin(SearchParameters searchParameters);
	}
}