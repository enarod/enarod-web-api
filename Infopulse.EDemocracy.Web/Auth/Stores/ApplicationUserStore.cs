using Microsoft.AspNet.Identity.EntityFramework;

namespace Infopulse.EDemocracy.Web.Auth
{
	public class ApplicationUserStore : UserStore<
		ApplicationUser,
		ApplicationRole,
		int,
		ApplicationUserLogin,
		ApplicationUserRole,
		ApplicationUserClaim>
	{
		public ApplicationUserStore(AuthContext context)
			: base(context)
		{
		}
	}
}