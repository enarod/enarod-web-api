using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.ServiceModel.Channels;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Infopulse.EDemocracy.Common.Extensions;

#pragma warning disable 1591

namespace Infopulse.EDemocracy.Web.Auth
{
	public class AuthorizationCheckFilter : ActionFilterAttribute
	{
		public override bool AllowMultiple => true;

		/// <summary>
		/// If current user has any of this role then action is authorized.
		/// </summary>
		public string RequiredRoles { get; set; }

		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (string.IsNullOrWhiteSpace(RequiredRoles)) return;

			var user = actionContext.RequestContext.Principal as ClaimsPrincipal;
			var roleClaims = user?.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

			if (!string.IsNullOrWhiteSpace(roleClaims?.Value))
			{
				var userRoles = roleClaims.Value.SplitStringBySeparator();
				var requiredRoles = this.RequiredRoles.SplitStringBySeparator();

				foreach (var requiredRole in requiredRoles)
				{
					if (!userRoles.Contains(requiredRole))
					{
						actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
						break;
					}
				}
			}

			base.OnActionExecuting(actionContext);
		}

		
	}
}