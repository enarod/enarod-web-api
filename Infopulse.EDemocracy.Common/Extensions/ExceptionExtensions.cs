using System;

namespace Infopulse.EDemocracy.Common.Extensions
{
	public static class ExceptionExtensions
	{
		public static Exception GetMostInnerException(this Exception exception)
		{
			var innerException = exception;

			while (innerException.InnerException != null)
			{
				innerException = innerException.InnerException;
			}

			return innerException;
		}
	}
}