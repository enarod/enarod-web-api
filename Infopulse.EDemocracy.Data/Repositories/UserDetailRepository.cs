using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class UserDetailRepository : IUserDetailRepository
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
				user.ID = userDetailFromDb.ID;

				db.Entry(userDetailFromDb).CurrentValues.SetValues(user);				
				db.SaveChanges();

				return db.UserDetails.SingleOrDefault(ud => ud.ID == user.ID);
			}
		}
	}
}