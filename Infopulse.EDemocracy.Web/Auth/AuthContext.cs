using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infopulse.EDemocracy.Web.Auth
{
	public class AuthContext : IdentityDbContext<IdentityUser>
	{
		public AuthContext()
			: base("AuthContext")
		{

		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<AppUser>().ToTable("User");
			base.OnModelCreating(modelBuilder);
		}
	}
}