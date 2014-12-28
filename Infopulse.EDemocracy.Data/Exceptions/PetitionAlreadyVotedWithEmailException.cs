using System;

namespace Infopulse.EDemocracy.Data.Exceptions
{
	public class PetitionAlreadyVotedWithEmailException : Exception
	{
		public PetitionAlreadyVotedWithEmailException()
			: base("This petition was already voted with this email.")
		{
			
		}
	}
}