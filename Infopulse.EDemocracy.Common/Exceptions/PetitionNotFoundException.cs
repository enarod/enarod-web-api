using Infopulse.EDemocracy.Common.Resources;

namespace Infopulse.EDemocracy.Common.Exceptions
{
	public class PetitionNotFoundException : DomainException
	{
		public PetitionNotFoundException()
			: base(Errors.PetitionNofFoundException, ExceptionLevel.Database)
		{
			
		}


		public PetitionNotFoundException(string message)
			: base(Errors.PetitionNofFoundException, message, ExceptionLevel.Database)
		{

		}
	}
}