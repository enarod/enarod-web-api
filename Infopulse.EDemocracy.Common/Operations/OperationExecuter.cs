using Infopulse.EDemocracy.Common.Exceptions;
using Infopulse.EDemocracy.Common.Extensions;
using Infopulse.EDemocracy.Common.Resources;
using System;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;

namespace Infopulse.EDemocracy.Common.Operations
{
	public class OperationExecuter
	{
		public static readonly int DbConnectionFailedCode;
		public static readonly string DbConnectionFailedMessage;

		static OperationExecuter()
		{
			OperationExecuter.DbConnectionFailedCode = int.Parse(Errors.DbConnectionFailedCode);
			OperationExecuter.DbConnectionFailedMessage = Errors.DbConnectionFailedMessage;
		}

		public static OperationResult Execute(Func<OperationResult> procedure)
		{
			OperationResult result;
			
			try
			{
				result = procedure.Invoke();
			}
			catch (DomainException exc)
			{
				result = OperationResult.ExceptionResult(exc);
			}
			catch (EntityException entityException)
			{
				result = OperationExecuter.GetDbConnectionFailedResult();
			}
			catch (DbEntityValidationException exception)
			{
				var errorMessage = string.Empty;

				foreach (var error in exception.EntityValidationErrors)
				{
					foreach (var validationError in error.ValidationErrors)
					{
						errorMessage += string.Format("{0}; ", validationError.ErrorMessage);
					}					
				}

				result = OperationResult.ExceptionResult(errorMessage, -3);
			}
			catch (Exception exception)
			{
				result = OperationResult.ExceptionResult(exception);
			}

			return result;
		}


		public static OperationResult<T> Execute<T>(Func<OperationResult<T>> procedure)
		{
			OperationResult<T> result = null;

			try
			{
				result = procedure.Invoke();
			}
			catch (DomainException exc)
			{
				result = OperationResult<T>.ExceptionResult(exc);
			}
			catch (EntityException entityException)
			{
				var innerMessage = entityException.GetMostInnerException().Message;
				result = OperationExecuter.GetDbConnectionFailedResult<T>(innerMessage);
			}
			catch (Exception exception)
			{
				result = OperationResult<T>.ExceptionResult(exception);
			}

			return result;
		}


		private static OperationResult GetDbConnectionFailedResult()
		{
			return OperationResult.Fail(DbConnectionFailedCode, DbConnectionFailedMessage);
		}


		private static OperationResult<T> GetDbConnectionFailedResult<T>(string innerMessage = null)
		{
			var result = OperationResult<T>.Fail(DbConnectionFailedCode, DbConnectionFailedMessage);
			if (!string.IsNullOrWhiteSpace(innerMessage))
			{
				result.DebugMessage = innerMessage;
			}

			return result;
		} 
	}
}