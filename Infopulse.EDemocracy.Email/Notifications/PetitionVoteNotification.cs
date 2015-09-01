using System.Collections.Generic;
using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Common.Resources;

namespace Infopulse.EDemocracy.Email.Notifications
{
	public class PetitionVoteNotification : NotificationBase
	{
		public PetitionVoteNotification(PetitionEmailVote emailVote) :
			base(
			Action.PetitionEmailVote,
			emailVote.PetitionSigner.User.Email,
			new Dictionary<string, string>()
			{
				{"PetitionName", emailVote.Petition.Subject },
				{"PetitionText", emailVote.Petition.Text},
				{"PetitionRequirements", emailVote.Petition.Requirements},
				{"VoteUrl", emailVote.ConfirmUrl},
				{"PetitionUrl", emailVote.Petition.Url},
				{"PetitionLimit", emailVote.Petition.Limit.ToString()}
			},
			string.Format(PetitionVoteOperationResult.VoteCreatedFormat, emailVote.PetitionSigner.User.Email))
		{

		}
	}
}