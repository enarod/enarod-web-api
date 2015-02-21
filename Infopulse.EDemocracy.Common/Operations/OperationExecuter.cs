using Infopulse.EDemocracy.Common.Exceptions;
using Infopulse.EDemocracy.Model.Resources;
using System;
using System.Data.Entity.Core;

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
				result = OperationExecuter.GetDbConnectionFailedResult<T>();
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


		private static OperationResult<T> GetDbConnectionFailedResult<T>()
		{
			return OperationResult<T>.Fail(DbConnectionFailedCode, DbConnectionFailedMessage);
		} 
	}
}