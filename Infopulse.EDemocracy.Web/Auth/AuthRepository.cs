using Infopulse.EDemocracy.Web.Auth;
using Infopulse.EDemocracy.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class AuthRepository : IDisposable
	{
		private AuthContext authContext;

		private ApplicationUserManager userManager;

		public AuthRepository()
		{
			authContext = new AuthContext();
			userManager = new ApplicationUserManager(new CustomUserStore(authContext));
		}

		public async Task<IdentityResult> RegisterUser(UserModel userModel)
		{
			try
			{
				var user = new ApplicationUser
				{
					UserName = userModel.UserEmail,
					Email = userModel.UserEmail,
					EmailConfirmed = false
				};

				var result = await userManager.CreateAsync(user, userModel.Password);

				return result;
			}
			catch (Exception exp)
			{
				throw;
			}			
		}

		public async Task<ApplicationUser> FindUser(string userName, string password)
		{
			var user = await userManager.FindAsync(userName, password);

			return user;
		}

		public void Dispose()
		{
			authContext.Dispose();
			userManager.Dispose();
		}
	}
}