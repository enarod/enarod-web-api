using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities;
using Infopulse.EDemocracy.Model.Common;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionVoteRepository
	{
		OperationResult Vote(ClientPetitionVote vote, string certificateSerialNumber);
		OperationResult<Model.BusinessEntities.PetitionEmailVote> EmailVote(EmailVote vote);
		OperationResult<Model.BusinessEntities.PetitionEmailVote> ConfirmPetitionEmailVote(string hash);
		OperationResult<Model.BusinessEntities.Petition> GetPetition(string hash);
		OperationResult ClearVotes();
		OperationResult ClearVote(int petitionID);
	}
}