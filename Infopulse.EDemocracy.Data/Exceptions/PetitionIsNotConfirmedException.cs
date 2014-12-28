using System;

namespace Infopulse.EDemocracy.Data.Exceptions
{
	public class PetitionIsNotConfirmedException : Exception
	{
		public PetitionIsNotConfirmedException()
			: base("Petition has already been voted, but not confirmed via email. Check your mail box.")
		{
			
		}
	}
}