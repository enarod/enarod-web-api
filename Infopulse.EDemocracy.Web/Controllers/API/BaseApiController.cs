using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Claims;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
    /// <summary>
    /// The base class for api controllers
    /// </summary>
    public abstract class BaseApiController : ApiController
    {
		protected IUserDetailRepository userDetailRepository;

	    protected BaseApiController()
		{
			this.userDetailRepository = new UserDetailRepository();
        }

		/// <summary>
		/// Gets UserEmail of signed in user.
		/// </summary>
		/// <returns>Signed in user's email.</returns>
		protected string GetSignedInUserEmail()
		{
			var identity = User.Identity as ClaimsIdentity;
			if (identity != null)
			{
				var emailClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email);

				if (emailClaim == null)
				{
					throw new AuthenticationException("Email claim not found");
				}

				var email = emailClaim.Value;
				return email;
			}

			return null;
		}

		/// <summary>
		/// Gets ID of signed in user.
		/// </summary>
		/// <returns>User ID.</returns>
		protected int GetSignedInUserId()
		{
			var userEmail = GetSignedInUserEmail();
			var userId = userDetailRepository.GetUserId(userEmail);
			return userId;
		}


		protected string GetClientIP(HttpRequestMessage request = null)
		{
			request = request ?? Request;

			var ip = Request.GetOwinContext().Request.RemoteIpAddress; // WebAPI 2.2 feature

			if (!string.IsNullOrWhiteSpace(ip)) return ip;

			if (request.Properties.ContainsKey("MS_HttpContext"))
			{
				return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
			}
			else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
			{
				RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
				return prop.Address;
			}
			else if (HttpContext.Current != null)
			{
				return HttpContext.Current.Request.UserHostAddress;
			}
			else
			{
				return null;
			}
		}
	}
}