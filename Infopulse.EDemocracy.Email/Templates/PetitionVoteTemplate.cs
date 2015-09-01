using Infopulse.EDemocracy.Common.Resources;

namespace Infopulse.EDemocracy.Email.Templates
{
	public class PetitionVoteTemplate : Template
	{
		public PetitionVoteTemplate()
			: base(StringConstants.PetitionVotedNotificationEmailSubject, "PetitionVoteConfirmation")
		{
			
		}
	}
}