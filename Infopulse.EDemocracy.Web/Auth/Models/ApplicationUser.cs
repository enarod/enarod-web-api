using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infopulse.EDemocracy.Web.Auth
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
	{
		
	}

	public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
	{
		public ApplicationRole() { }
		public ApplicationRole(string name) { Name = name; }
	}

	public class ApplicationUserRole : IdentityUserRole<int> { }
	public class ApplicationUserLogin : IdentityUserLogin<int> { }

	public class ApplicationUserClaim : IdentityUserClaim<int> { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}