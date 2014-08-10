using System;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace Infopulse.EDemocracy.Web.CORS
{
	/// <summary>
	/// Custom CORS policy provider based on web.config values.
	/// </summary>
	/// <remarks>See http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api#cors-policy-providers for details.</remarks>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
	public class CorsPolicyProvider : Attribute, ICorsPolicyProvider
	{
		private CorsPolicy _policy;

		public CorsPolicyProvider()
		{
			_policy = new CorsPolicy
			{
				AllowAnyMethod = true,
				AllowAnyHeader = true
			};

			// Add allowed origins.
			var origins = ConfigurationManager.AppSettings["CorsOrigins"];
			if (origins.Equals("*"))
			{
				_policy.AllowAnyOrigin = true;
			}
			else
			{
				foreach (var origin in origins.Split(';'))
				{
					if (!string.IsNullOrWhiteSpace(origin)) _policy.Origins.Add(origin);
				}
			}
		}


		public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken token)
		{
			return Task.FromResult(_policy);
		}
	}
}