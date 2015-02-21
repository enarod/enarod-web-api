using System;
using System.Data.Entity.Core;
using System.Runtime.CompilerServices;
using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.Common;
using Infopulse.EDemocracy.Model.Resources;

namespace Infopulse.EDemocracy.Data
{
	/// <summary>
	/// DB execute wrapper. Catches DB connection error. Loging DB queries.
	/// </summary>
	public static class DbExecuter
	{
		public static readonly int DbConnectionFailedCode;
		public static readonly string DbConnectionFailedMessage;

		static DbExecuter()
		{
			DbExecuter.DbConnectionFailedCode = int.Parse(Errors.DbConnectionFailedCode);
			DbExecuter.DbConnectionFailedMessage = Errors.DbConnectionFailedMessage;
		}

		public static OperationResult Execute(Func<EDEntities, OperationResult> procedure, [CallerMemberName]string methodName = null)
		{
			OperationResult result;
			
			try
			{
				using (var db = new EDEntities())
				{
					db.Database.Log = s => DbLog.Add(s, methodName);
					result = procedure.Invoke(db);
				}
			}
			catch (EntityException entityException)
			{
				result = DbExecuter.GetDbConnectionFailedResult();
			}
			catch (Exception exception)
			{
				result = OperationResult.ExceptionResult(exception);
			}

			return result;
		}


		public static OperationResult<T> Execute<T>(Func<EDEntities, OperationResult<T>> procedure, [CallerMemberName]string methodName = null)
		{
			OperationResult<T> result;

			try
			{
				using (var db = new EDEntities())
				{
					db.Database.Log = s => DbLog.Add(s, methodName);
					result = procedure.Invoke(db);
				}
			}
			catch (EntityException entityException)
			{
				result = DbExecuter.GetDbConnectionFailedResult<T>();
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