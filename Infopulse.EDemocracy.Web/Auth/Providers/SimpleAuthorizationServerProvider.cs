using System;
using System.Data.Entity;
using System.Linq;
using Infopulse.EDemocracy.Data.Repositories;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using Infopulse.EDemocracy.Common.Extensions;
using Infopulse.EDemocracy.Model.Enum;

namespace Infopulse.EDemocracy.Web.Auth.Providers
{
	public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

			using (AuthRepository _repo = new AuthRepository())
			{
				var user = await _repo.FindUser(context.UserName, context.Password);

				if (user == null)
				{
					context.SetError("invalid_grant", "The user name or password is incorrect.");
					return;
				}
			}

			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			identity.AddClaim(new Claim(ClaimTypes.Email, context.UserName));
			AddRoleClaim(identity, context.UserName);

			context.Validated(identity);
		}

		private void AddRoleClaim(ClaimsIdentity identity, string userEmail)
		{
			var roleClaim = GetUserRoleClaim(userEmail);
			if (roleClaim != null)
			{
				identity.AddClaim(roleClaim);
			}
		}

		private Claim GetUserRoleClaim(string userEmail)
		{
			var roleClaimValue = this.GetUserRolesAsString(userEmail);
			return new Claim(ClaimTypes.Role, roleClaimValue ?? string.Empty);
		}

		private string GetUserRolesAsString(string userEmail)
		{
			using (var db = new AuthContext())
			{
				var user = db.Users.Include(u => u.Roles).SingleOrDefault(u => u.Email == userEmail);
				if (user == null || !user.Roles.Any()) return null;

				var userRoles = user.Roles.Select(r => r.RoleId.AsRoleText());
				var roles = string.Join(",", userRoles);
				return roles;
			}
		}
	}
}