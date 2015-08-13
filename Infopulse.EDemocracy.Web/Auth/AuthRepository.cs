using Infopulse.EDemocracy.Web.Auth;
using Infopulse.EDemocracy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class AuthRepository : IDisposable
	{
		private AuthContext authContext;

		private UserManager<IdentityUser> userManager;

		public AuthRepository()
		{
			authContext = new AuthContext();
			userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(authContext));
		}

		public async Task<IdentityResult> RegisterUser(UserModel userModel)
		{
			var user = new IdentityUser
			{
				UserName = userModel.UserName
				//,
				//PetitionSignerId = null
			};

			var result = await userManager.CreateAsync(user, userModel.Password);

			return result;
		}

		public async Task<IdentityUser> FindUser(string userName, string password)
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