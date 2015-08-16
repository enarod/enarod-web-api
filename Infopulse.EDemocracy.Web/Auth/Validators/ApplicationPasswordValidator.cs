using Microsoft.AspNet.Identity;

namespace Infopulse.EDemocracy.Web.Auth.Validators
{
	public class ApplicationPasswordValidator : PasswordValidator
	{
		public ApplicationPasswordValidator()
		{
			RequiredLength = 3;
			RequireNonLetterOrDigit = false;
			RequireDigit = false;
			RequireLowercase = false;
			RequireUppercase = false;
		}
	}
}