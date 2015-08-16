using Microsoft.AspNet.Identity;

namespace Infopulse.EDemocracy.Web.Auth.Validators
{
	public class ApplicationUserValidator : UserValidator<ApplicationUser, int>
	{
		public ApplicationUserValidator(ApplicationUserManager manager) : base(manager)
		{
			AllowOnlyAlphanumericUserNames = false;
			RequireUniqueEmail = true;
        }
	}
}