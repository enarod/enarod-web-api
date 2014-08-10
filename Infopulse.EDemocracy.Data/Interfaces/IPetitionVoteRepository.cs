using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities;
using Infopulse.EDemocracy.Model.Common;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionVoteRepository
	{
		OperationResult Vote(ClientPetitionVote vote, string certificateSerialNumber);
		OperationResult EmailVote(EmailVote vote);
		OperationResult<PetitionVote> ConfirmPetitionEmailVote(string hash);
		OperationResult<Petition> GetPetition(string hash);
		OperationResult ClearVotes();
		OperationResult ClearVote(int petitionID);
	}
}