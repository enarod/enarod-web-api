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
			return "pong from " + this.GetSignedInUserEmail();
		}
	}
}