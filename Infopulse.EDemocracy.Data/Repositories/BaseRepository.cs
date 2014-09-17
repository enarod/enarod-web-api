using System.Linq;
using System.Runtime.CompilerServices;
using Infopulse.EDemocracy.Model;

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
    }
}