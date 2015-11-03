using System.Linq;
using System.Web.Http;
using Infopulse.EDemocracy.Web.Auth;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
	public class TestController : BaseApiController
	{
		[HttpGet]
		[Authorize]
		[AuthorizationCheckFilter(RequiredRolesString = "admin")]
		[Route("api/test/ping")]
		public string Ping()
		{
			var repo = new Infopulse.EDemocracy.Data.Repositories.OrganizationRepository();
			var organization = repo.GetAll().FirstOrDefault();
			return "понґ від " + this.GetSignedInUserEmail() + " з організації '" + (organization?.Name ?? "unknown") + "'";
		}
	}
}