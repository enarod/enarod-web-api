using System;

namespace Infopulse.EDemocracy.Common.Exceptions
{
	public abstract class DomainException : Exception
	{
		public ExceptionLevel ExceptionLevel { get; set; }
		public string DisplayMessage { get; set; }
		
		protected DomainException(string displayName, ExceptionLevel exceptionLevel = ExceptionLevel.Other)
		{
			this.DisplayMessage = displayName;
			this.ExceptionLevel = exceptionLevel;
		}

		protected DomainException(string displayName, string debugMessage, ExceptionLevel exceptionLevel = ExceptionLevel.Other)
			: base(debugMessage)
		{
			this.DisplayMessage = displayName;
			this.ExceptionLevel = exceptionLevel;
		}
	}
}