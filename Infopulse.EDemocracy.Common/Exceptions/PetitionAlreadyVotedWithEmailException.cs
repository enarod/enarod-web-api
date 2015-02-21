namespace Infopulse.EDemocracy.Common.Exceptions
{
	public class PetitionAlreadyVotedWithEmailException : DomainException
	{
		public PetitionAlreadyVotedWithEmailException()
			: base("Ви вже проголосували за цю петицію.", ExceptionLevel.BusinnessLogic)
		{
			
		}
	}
}