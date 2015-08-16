using Infopulse.EDemocracy.Common.Extensions;
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
		private ApplicationUserManager applicationUserManager;

		public AuthRepository()
		{
			authContext = new AuthContext();
			applicationUserManager = new ApplicationUserManager(new ApplicationUserStore(authContext));
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

				var result = await applicationUserManager.CreateAsync(user, userModel.Password);

				return result;
			}
			catch (Exception exc)
			{
				throw exc.GetMostInnerException();
			}			
		}

		public async Task<ApplicationUser> FindUser(string userName, string password)
		{
			var user = await applicationUserManager.FindAsync(userName, password);

			return user;
		}

		public void Dispose()
		{
			authContext.Dispose();
			applicationUserManager.Dispose();
		}
	}
}