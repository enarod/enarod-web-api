namespace Infopulse.EDemocracy.Common.Exceptions
{
	public class PetitionIsNotConfirmedException : DomainException
	{
		public PetitionIsNotConfirmedException()
			: base("Ви вже проголосували за петицію, але не підтвердили голосування через електронну пошту. Будь-ласка, перевірте вашу поштову скринську.", ExceptionLevel.BusinnessLogic)
		{
			
		}
	}
}