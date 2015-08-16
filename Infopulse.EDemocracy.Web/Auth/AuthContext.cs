using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Infopulse.EDemocracy.Web.Auth.Models;

namespace Infopulse.EDemocracy.Web.Auth
{
	public class AuthContext : IdentityDbContext<
		ApplicationUser,
		ApplicationRole,
		int,
		ApplicationUserLogin,
		ApplicationUserRole,
		ApplicationUserClaim>
	{
		public AuthContext()
			: base("AuthContext")
		{
			Database.SetInitializer(new CreateDatabaseIfNotExists<AuthContext>());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ApplicationUser>().ToTable("App_User", "auth");
			modelBuilder.Entity<ApplicationRole>().ToTable("App_Role", "auth");

			modelBuilder.Entity<ApplicationUserRole>().ToTable("App_UserRole", "auth");
			modelBuilder.Entity<ApplicationUserLogin>().ToTable("App_UserLogin", "auth");
			modelBuilder.Entity<ApplicationUserClaim>().ToTable("App_UserClaim", "auth");
		}
	}
}