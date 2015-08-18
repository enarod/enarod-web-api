namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IUserDetailRepository
	{
		int GetUserId(string userEmail);
	}
}
