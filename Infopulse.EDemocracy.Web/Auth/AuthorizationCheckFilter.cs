using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Infopulse.EDemocracy.Common.Extensions;
using Infopulse.EDemocracy.Model.Enum;

#pragma warning disable 1591

namespace Infopulse.EDemocracy.Web.Auth
{
	/// <summary>
	/// Checks authorization of the user for specific criteria.
	/// </summary>
	/// <remarks>
	/// Allows mutiple check. If user must have all of two roles - then use multiple attributes:
	/// <code>
	///		[AuthorizationCheckFilter(RequiredRoleString="role1")]
	///		[AuthorizationCheckFilter(RequiredRoleString="role1")]
	///		public HttpResponseMessage Action() { ... }
	/// </code>
	/// If user can have any role from list - then use comma separated string:
	/// <code>
	///		[AuthorizationCheckFilter(RequiredRoleString="role1,role2")]
	///		public HttpResponseMessage Action() { ... }
	/// </code>
	/// </remarks>
	public class AuthorizationCheckFilter : ActionFilterAttribute
	{
		public override bool AllowMultiple => true;

		/// <summary>
		/// If current user has any of these roles then the action is authorized.
		/// </summary>
		public string RequiredRolesString { get; set; }

		/// <summary>
		/// If current user has any of these roles then the action is authorized.
		/// </summary>
		public Role[] RequiredRoles { get; set; }

		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			var requiredRoles = this.GetRequiredRoles();
			if (!requiredRoles.Any()) return;

			var user = actionContext.RequestContext.Principal as ClaimsPrincipal;
			var roleClaims = user?.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

			if (!string.IsNullOrWhiteSpace(roleClaims?.Value))
			{
				var userRoles = roleClaims.Value.SplitStringBySeparator();

				var userHasAtLeastOneRequiredRole = false;
				foreach (var requiredRole in requiredRoles)
				{
					if (userRoles.Contains(requiredRole))
					{
						userHasAtLeastOneRequiredRole = true;
						break;
					}
				}

				if (!userHasAtLeastOneRequiredRole)
				{
					actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
				}
			}

			base.OnActionExecuting(actionContext);
		}

		////private string GetRolesString()
		////{
		////	if (string.IsNullOrWhiteSpace(this.RequiredRolesString))
		////	{
		////		var requiredRolesString = new StringBuilder();
		////		for (int i = 0; i < this.RequiredRoles.Count; i++)
		////		{
		////			requiredRolesString.AppendFormat("{0},", this.RequiredRoles[i]);
		////		}

		////		var result = requiredRolesString.Remove(requiredRolesString.Length - 2, 1).ToString();
		////		return result;
		////	}
		////	else
		////	{
		////		return this.RequiredRolesString;
		////	}
		////}

		private List<string> GetRequiredRoles()
		{
			List<string> result;
			if (!string.IsNullOrWhiteSpace(this.RequiredRolesString))
			{
				result = this.RequiredRolesString.SplitStringBySeparator();
			}
			else if (this.RequiredRoles.Any())
			{
				result = this.RequiredRoles.Select(rr => rr.ToString()).ToList();
			}
			else
			{
				result = new List<string>();
			}

			return result.Select(r => r.ToLower()).ToList();
		}
	}
}