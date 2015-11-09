using Infopulse.EDemocracy.Common.Extensions;
using Infopulse.EDemocracy.Web.Auth;
using Infopulse.EDemocracy.Web.Auth.Models;
using Infopulse.EDemocracy.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Infopulse.EDemocracy.Model.Enum;

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

		public async Task<IdentityResult> ChangePassword(int userID, string currentPassword, string newPassword)
		{
			try
			{
				var result = await applicationUserManager.ChangePasswordAsync(userID, currentPassword, newPassword);

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

		public void AssignRole(string userEmail, params Role[] roles)
		{
			var user = authContext.Users
				.Include(u => u.Roles)
				.SingleOrDefault(u => u.Email == userEmail);
			if(user == null) throw new Exception($"User [{userEmail}] not found.");

			var userRoleIds = user.Roles.Select(ur => ur.RoleId).ToList();
            foreach (var role in roles.Where(r => !userRoleIds.Contains((int)r)))
			{
				user.Roles.Add(new ApplicationUserRole() { UserId = user.Id, RoleId = (int)role });
			}

			authContext.SaveChanges();
		}

		public void RemoveRole(string userEmail, params Role[] roles)
		{
			var user = authContext.Users
				.Include(u => u.Roles)
				.SingleOrDefault(u => u.Email == userEmail);
			if (user == null) throw new Exception($"User [{userEmail}] not found.");

			var userRoleIds = user.Roles.Select(ur => ur.RoleId).ToList();
			foreach (var roleToDelete in roles.Where(r => userRoleIds.Contains((int)r)).Select(role => user.Roles.SingleOrDefault(ur => ur.RoleId == (int)role)))
			{
				user.Roles.Remove(roleToDelete);
			}

			authContext.SaveChanges();
		}

		public void Dispose()
		{
			authContext.Dispose();
			applicationUserManager.Dispose();
		}
	}
}