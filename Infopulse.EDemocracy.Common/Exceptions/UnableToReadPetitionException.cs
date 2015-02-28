using Infopulse.EDemocracy.Common.Resources;

namespace Infopulse.EDemocracy.Common.Exceptions
{
	public class UnableToReadPetitionException : DomainException
	{
		public UnableToReadPetitionException()
			: base(Errors.UnableToReadPetitionException, ExceptionLevel.Database)
		{
			
		}


		public UnableToReadPetitionException(string message)
			: base(Errors.UnableToReadPetitionException, message, ExceptionLevel.Database)
		{

		}
	}
}