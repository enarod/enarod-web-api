using Microsoft.AspNet.Identity.EntityFramework;

namespace Infopulse.EDemocracy.Web.Auth
{
	public class ApplicationRoleStore : RoleStore<ApplicationRole, int, ApplicationUserRole>
	{
		public ApplicationRoleStore(AuthContext context)
			: base(context)
		{
		}
	}
}