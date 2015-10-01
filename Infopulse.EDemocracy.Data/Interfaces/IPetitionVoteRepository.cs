using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities.Search;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionVoteRepository
	{
		PetitionEmailVote CreateEmailVoteRequest(PetitionEmailVote emailVote);
		PetitionEmailVote ConfirmEmailVoteRequest(string hash);

		PetitionEmailVote CreateRecallVoteRequest(PetitionEmailVote emailVote);
		PetitionEmailVote ConfirmRecallVoteRequest(PetitionEmailVote emailVote);

		void ClearVotes();
		void ClearVote(int petitionID);

		IEnumerable<PetitionEmailVote> GetPetitionVotes(int petitionID, SearchParameters searchParameters);
	}
}