using Infopulse.EDemocracy.Common.Resources;

namespace Infopulse.EDemocracy.Common.Exceptions
{
	public class PetitionAlreadyVotedWithEmailException : DomainException
	{
		public PetitionAlreadyVotedWithEmailException()
			: base(Errors.PetitionAlreadyVotedException, ExceptionLevel.BusinnessLogic)
		{
			
		}


		public PetitionAlreadyVotedWithEmailException(string message)
			: base(Errors.PetitionAlreadyVotedException, message, ExceptionLevel.BusinnessLogic)
		{

		}
	}
}