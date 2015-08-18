using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
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
		protected IUserDetailRepository userDetailRepository;

		public BaseApiController()
		{
			this.userDetailRepository = new UserDetailRepository();
        }

		/// <summary>
		/// Gets UserEmail of signed in user.
		/// </summary>
		/// <returns>Signed in user's email.</returns>
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

		/// <summary>
		/// Gets ID of signed in user.
		/// </summary>
		/// <returns>User ID.</returns>
		public int GetSignedInUserId()
		{
			var userEmail = GetSignedInUserEmail();
			var userId = userDetailRepository.GetUserId(userEmail);
			return userId;
		}
    }
}