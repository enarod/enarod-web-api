using Infopulse.EDemocracy.Model.ClientEntities;
using Infopulse.EDemocracy.Model.Common;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionVoteRepository
	{
		OperationResult Vote(ClientPetitionVote vote, string certificateSerialNumber);
		

		/// <summary>
		/// Saves email vote request as pair of Email-PetitionID.
		/// </summary>
		/// <param name="vote">Email vote.</param>
		/// <returns>Operation result of creation new record in DB.</returns>
		OperationResult<Model.BusinessEntities.PetitionEmailVote> CreateEmailVoteRequest(EmailVote vote);


		/// <summary>
		/// Confirms email vote request by EmailVote hash.
		/// </summary>
		/// <param name="hash">PetitionID-Email hash.</param>
		/// <returns>Operation result of updating (satisfying) EmailVote request in DB.</returns>
		OperationResult<Model.BusinessEntities.PetitionEmailVote> ConfirmEmailVoteRequest(string hash);
		OperationResult<Model.BusinessEntities.Petition> GetPetition(string hash);
		OperationResult ClearVotes();
		OperationResult ClearVote(int petitionID);
	}
}