using System.Collections.Generic;
using Infopulse.EDemocracy.Model;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IUserDetailRepository
	{
		int GetUserId(string userEmail);
		UserDetail Get(int userID);
		UserDetail Update(UserDetail user);

		//IEnumerable<UserDetail> Get();
	}
}
