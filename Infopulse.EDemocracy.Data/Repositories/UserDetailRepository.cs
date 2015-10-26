using System;
using System.Collections.Generic;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class UserDetailRepository : BaseRepository, IUserDetailRepository
	{
		public int GetUserId(string userEmail)
		{
			using (var db = new EDEntities())
			{
				var userID = db.Database.SqlQuery<int>(
					"sp_User_GetIdByEmail @UserEmail",
					new SqlParameter()
					{
						ParameterName = "UserEmail",
						DbType = DbType.String,
						Value = userEmail,
						Direction = ParameterDirection.Input
					});

				return userID.SingleOrDefault();
			}
		}

		public UserDetail Update(UserDetail user)
		{
			using (var db = new EDEntities())
			{
				var userDetailFromDb = db.UserDetails.SingleOrDefault(ud => ud.UserID == user.UserID);
				if (userDetailFromDb == null && user.User != null && !string.IsNullOrWhiteSpace(user.User.Email))
				{
					userDetailFromDb = db.UserDetails.SingleOrDefault(ud => ud.User.Email == user.User.Email);
				}

				if (userDetailFromDb == null)
				{
					user.CreatedBy = this.UnknownAppUser;
					user.CreatedDate = DateTime.UtcNow;
					user.User = null;
					db.UserDetails.Add(user);
				}
				else
				{
					user.ID = userDetailFromDb.ID;

					var entry = db.Entry(userDetailFromDb);
					entry.CurrentValues.SetValues(user);
					entry.Property(u => u.CreatedBy).IsModified = false;
					entry.Property(u => u.CreatedDate).IsModified = false;
				}

				db.SaveChanges();

				return db.UserDetails.SingleOrDefault(ud => ud.ID == user.ID);
			}
		}

		//public IEnumerable<UserDetail> Get()
		//{
		//	using (var db = new EDEntities())
		//	{
		//		var usersInfo = db.UserDetails
					//.Include("User")
					//.ToList();
		//		return usersInfo;
		//	}
		//}

		public UserDetail Get(int userID)
		{
			using (var db = new EDEntities())
			{
				var userInfo = db.UserDetails
					.Include("User")
					.SingleOrDefault(u => u.UserID == userID);
				return userInfo;
			}
		}
	}
}