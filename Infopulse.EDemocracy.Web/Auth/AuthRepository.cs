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

#pragma warning disable 1591

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class AuthRepository : IDisposable
	{
		private readonly AuthContext _authContext;
		private readonly ApplicationUserManager _applicationUserManager;

		public AuthRepository()
		{
			_authContext = new AuthContext();
			_applicationUserManager = new ApplicationUserManager(new ApplicationUserStore(_authContext));
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

				var result = await _applicationUserManager.CreateAsync(user, userModel.Password);

				return result;
			}
			catch (Exception exc)
			{
				throw exc.GetMostInnerException();
			}			
		}

		public async Task<IdentityResult> ChangePasswordAsync(int userID, string currentPassword, string newPassword)
		{
			try
			{
				var result = await _applicationUserManager.ChangePasswordAsync(userID, currentPassword, newPassword);

				return result;
			}
			catch (Exception exc)
			{
				throw exc.GetMostInnerException();
			}
		}

		public IdentityResult ChangePassword(int userID, string currentPassword, string newPassword)
		{
			try
			{
				var result = _applicationUserManager.ChangePassword(userID, currentPassword, newPassword);

				return result;
			}
			catch (Exception exc)
			{
				throw exc.GetMostInnerException();
			}
		}

		public async Task<ApplicationUser> FindUser(string userName, string password)
		{
			var user = await _applicationUserManager.FindAsync(userName, password);

			return user;
		}

		public void AssignRole(string userEmail, params Role[] roles)
		{
			var user = _authContext.Users
				.Include(u => u.Roles)
				.SingleOrDefault(u => u.Email == userEmail);
			if(user == null) throw new Exception($"User [{userEmail}] not found.");

			var userRoleIds = user.Roles.Select(ur => ur.RoleId).ToList();
            foreach (var role in roles.Where(r => !userRoleIds.Contains((int)r)))
			{
				user.Roles.Add(new ApplicationUserRole() { UserId = user.Id, RoleId = (int)role });
			}

			_authContext.SaveChanges();
		}

		public void RemoveRole(string userEmail, params Role[] roles)
		{
			var user = _authContext.Users
				.Include(u => u.Roles)
				.SingleOrDefault(u => u.Email == userEmail);
			if (user == null) throw new Exception($"User [{userEmail}] not found.");

			var userRoleIds = user.Roles.Select(ur => ur.RoleId).ToList();
			foreach (var roleToDelete in roles.Where(r => userRoleIds.Contains((int)r)).Select(role => user.Roles.SingleOrDefault(ur => ur.RoleId == (int)role)))
			{
				user.Roles.Remove(roleToDelete);
			}

			_authContext.SaveChanges();
		}

		public void Dispose()
		{
			_authContext.Dispose();
			_applicationUserManager.Dispose();
		}
	}
}