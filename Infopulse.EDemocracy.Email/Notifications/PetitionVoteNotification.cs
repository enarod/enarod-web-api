using System.Collections.Generic;
using Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Email.Notifications
{
	public class PetitionVoteNotification : NotificationBase
	{
		public PetitionVoteNotification(PetitionEmailVote emailVote) :
			base(
			Action.PetitionEmailVote,
			emailVote.Email,
			new Dictionary<string, string>()
			{
				{"PetitionName", emailVote.Petition.Subject},
				{"PetitionText", emailVote.Petition.Text},
				{"PetitionRequirements", emailVote.Petition.Requirements},
				{"VoteUrl", emailVote.ConfirmUrl},
				{"PetitionUrl", emailVote.Petition.Url},
				{"PetitionLimit", emailVote.Petition.Limit.ToString()}
			},
			string.Format("��� ����������� ����������� �������� �� ����������, ���������� ��� �� email {0}", emailVote.Email)
			)
		{

		}
	}
}