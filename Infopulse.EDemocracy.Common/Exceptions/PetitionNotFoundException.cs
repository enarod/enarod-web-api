namespace Infopulse.EDemocracy.Common.Exceptions
{
	public class PetitionNotFoundException : DomainException
	{
		public PetitionNotFoundException()
			: base("Петиція не знайдена.", ExceptionLevel.Database)
		{
			
		}
	}
}