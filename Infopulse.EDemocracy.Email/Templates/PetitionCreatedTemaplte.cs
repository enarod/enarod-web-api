using Infopulse.EDemocracy.Common.Resources;

namespace Infopulse.EDemocracy.Email.Templates
{
	public class PetitionCreatedTemaplte : Template
	{
		public PetitionCreatedTemaplte()
			: base(StringConstants.PetitionCreatedNotificationEmailSubject, "PetitionCreateConfirmation")
		{
			
		}
	}
}