using System.Web;
using System.Web.Http;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
	public class TestController : BaseApiController
	{
		[HttpGet]
		[Authorize]
		[Route("api/test/ping")]
		public string Ping()
		{
			var repo = new Infopulse.EDemocracy.Data.Repositories.OrganizationRepository();
			var organization = repo.Get(5);
			return "понґ від " + this.GetSignedInUserEmail() + " з організації '" + organization.Name + "'";
		}
	}
}