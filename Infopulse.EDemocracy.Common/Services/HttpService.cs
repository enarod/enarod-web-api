using System.Net.Http;
using System.Threading.Tasks;

namespace Infopulse.EDemocracy.Common.Services
{
	public class HttpService
	{
		public async Task<string> GetStringAsync(string url)
		{
			using (var client = new HttpClient())
			{
				var requestTask = client.GetStringAsync(url).ConfigureAwait(false);
				return await requestTask;
			}
		}

		public async Task<HttpResponseMessage> GetAsync(string url)
		{
			using (var client = new HttpClient())
			{
				var requestTask = client.GetAsync(url).ConfigureAwait(false);
				return await requestTask;
			}
		}
	}
}