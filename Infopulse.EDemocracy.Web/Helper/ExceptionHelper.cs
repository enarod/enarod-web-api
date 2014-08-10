using System;
using System.Collections.Generic;
using System.Linq;

namespace Infopulse.EDemocracy.Web.Helper
{
	public static class ExceptionHelper
	{
		public static Exception GetInnerException(this Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}

			return ExceptionHelper.YieldInnerException(exception).SingleOrDefault();
		}

		
		private static IEnumerable<Exception> YieldInnerException(Exception exception)
		{
			var innerException = exception;
			do
			{
				yield return innerException;
				innerException = innerException.InnerException;
			}
			while (innerException != null);
		}
	}
}