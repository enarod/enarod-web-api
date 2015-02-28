using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Infopulse.EDemocracy.Data.Repositories
{
	/// <summary>
	/// Base class for repositories
	/// </summary>
	public class BaseRepository
	{
		internal Person GetAnonymousUser(EDEntities db)
		{
			var anonymousUser = db.People.SingleOrDefault(u => u.Login == "testuser") ?? new Person();
			return anonymousUser;
		}

		internal string GetMethodName()
		{
			return System.Reflection.MethodBase.GetCurrentMethod().Name;
		}

		internal void AddLogging(EDEntities db, [CallerMemberName]string methodName = null)
		{
			db.Database.Log = s => DbLog.Add(s, methodName);
		}

		protected string UnknownAppUser
		{
			get
			{
				return "Unknown app user";
			}
		}


		protected SqlParameter[] AddDefaultSearchParameters(SqlParameter[] sqlParameters, SearchParameters searchParameters)
		{
			var sqlParametersList = sqlParameters.ToList();
			sqlParametersList.AddRange(this.GetDefaultSearchParameters(searchParameters));
			return sqlParametersList.ToArray();
		}


		/// <summary>
		/// Gets PageNumber, PageSize and OrderBy SQL parameters.
		/// </summary>
		protected IEnumerable<SqlParameter> GetDefaultSearchParameters(SearchParameters searchParameters)
		{
			return new[]
			{
				new SqlParameter()
				{
					SqlDbType = SqlDbType.Int,
					Direction = ParameterDirection.Input,
					ParameterName = "PageNumber",
					Value = searchParameters.PageNumber.HasValue ? (object)searchParameters.PageNumber : DBNull.Value
				},
				new SqlParameter()
				{
					SqlDbType = SqlDbType.Int,
					Direction = ParameterDirection.Input,
					ParameterName = "PageSize",
					Value = searchParameters.PageSize.HasValue ? (object)searchParameters.PageSize : DBNull.Value
				},
				new SqlParameter()
				{
					SqlDbType = SqlDbType.NVarChar,
					Direction = ParameterDirection.Input,
					ParameterName = "OrderBy",
					Value = searchParameters.OrderBy +
						(
							string.IsNullOrWhiteSpace(searchParameters.OrderDirection)
								? string.Empty
								: (
									string.Equals(searchParameters.OrderDirection, "DESC", StringComparison.InvariantCultureIgnoreCase)
										? " DESC"
										: string.Empty
								)
						)
				},
			};
		}
	}
}