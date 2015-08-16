using Infopulse.EDemocracy.Web.Auth.Validators;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Infopulse.EDemocracy.Web.Auth
{
	public class ApplicationUserManager : UserManager<ApplicationUser, int>
	{
		public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
			: base(store)
		{
		}

		public static ApplicationUserManager Create(
			IdentityFactoryOptions<ApplicationUserManager> options,
			IOwinContext context)
		{
			var manager = new ApplicationUserManager(new ApplicationUserStore(context.Get<AuthContext>()));
			
			manager.UserValidator = new ApplicationUserValidator(manager);
			manager.PasswordValidator = new ApplicationPasswordValidator();
			
			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider =
					new DataProtectorTokenProvider<ApplicationUser, int>(
						dataProtectionProvider.Create("ASP.NET Identity"));
			}
			return manager;
		}
	}
}