using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
    /// <summary>
    /// The base class for api controllers
    /// </summary>
    public class BaseApiController : ApiController
    {
		public string GetSignedInUserEmail()
		{
			var identity = User.Identity as ClaimsIdentity;
			if (identity != null)
			{
				var emailClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email);
				var email = emailClaim.Value;
				return email;
			}

			return null;
		}
    }
}