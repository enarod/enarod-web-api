using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
    public class TestController : ApiController
    {
		[HttpGet]
		[Authorize]
		[Route("api/test")]
		public IEnumerable<Data> Get(int id)
		{
			return new List<Data>
			{
				new Data { ID = id, Text = "abc" },
				new Data { ID = id + 1, Text = "vvv" },
				new Data { ID = id + 2, Text = "qwe" }
			};
		}
	}

	public class Data
	{
		public int ID { get; set; }
		public string Text { get; set; }
	}
}