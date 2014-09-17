using System;
using System.Data.Entity.Core;
using Infopulse.EDemocracy.Model.Resources;

namespace Infopulse.EDemocracy.Model.Common
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
			OperationResult<T> result;

			try
			{
				result = procedure.Invoke();
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