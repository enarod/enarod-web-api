using Infopulse.EDemocracy.Model.BusinessEntities;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Email.eNarod.Notifications
{
	public class PetitionCreatedNotification : NotificationBase
	{
		public PetitionCreatedNotification(Petition createdPetition, string confirmCreationUrl) :
			base(
			Action.PetitionCreation,
			createdPetition.Email,
			new Dictionary<string, string>()
			{
				{"PetitionName", createdPetition.Subject},
				{"PetitionText", createdPetition.Text},
				{"PetitionRequirements", createdPetition.Requirements},
				{"ConfirmUrl", confirmCreationUrl},
				{"PetitionUrl", createdPetition.Url}
			},
			string.Format("��� ������������ ��������� ������� �������� �� ����������, ���������� ��� �� email {0}.", createdPetition.Email)
			)
		{

		}
	}
}