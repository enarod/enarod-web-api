using Infopulse.EDemocracy.Common.Resources;

namespace Infopulse.EDemocracy.Common.Exceptions
{
	public class PetitionVoteIsNotConfirmedException : DomainException
	{
		public PetitionVoteIsNotConfirmedException(string email)
			: base(string.Format(Errors.PetitionVoteNotConfirmedException_Mask, email), ExceptionLevel.BusinnessLogic)
		{
			
		}


		public PetitionVoteIsNotConfirmedException(string email, string message)
			: base(string.Format(Errors.PetitionVoteNotConfirmedException_Mask, email), message, ExceptionLevel.BusinnessLogic)
		{

		}
	}
}