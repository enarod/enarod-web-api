using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infopulse.EDemocracy.Web.Auth
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	[Table("Auth_User")]
	public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
	{
		
	}

	[Table("Auth_UserRole")]
	public class CustomUserRole : IdentityUserRole<int> { }
	[Table("Auth_UserClaim")]
	public class CustomUserClaim : IdentityUserClaim<int> { }
	[Table("Auth_UserLogin")]
	public class CustomUserLogin : IdentityUserLogin<int> { }

	[Table("Auth_Role")]
	public class CustomRole : IdentityRole<int, CustomUserRole>
	{
		public CustomRole() { }
		public CustomRole(string name) { Name = name; }
	}

	public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
	{
		public CustomUserStore(AuthContext context)
			: base(context)
		{
		}
	}

	public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
	{
		public CustomRoleStore(AuthContext context)
			: base(context)
		{
		}
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}