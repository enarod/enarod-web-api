using Microsoft.AspNet.Identity.EntityFramework;

namespace Infopulse.EDemocracy.Web.Auth.Models
{
	public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
	{
		
	}
}