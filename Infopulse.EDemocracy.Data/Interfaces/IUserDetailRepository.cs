using Infopulse.EDemocracy.Model;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IUserDetailRepository
	{
		int GetUserId(string userEmail);
		UserDetail Update(UserDetail user);
	}
}
