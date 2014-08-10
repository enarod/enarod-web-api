using System;
using System.Linq;
using System.Web.Http;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Web.CORS;
using Infopulse.EDemocracy.Web.Models;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
	[CorsPolicyProvider]
	public class CryptoController : BaseApiController
	{
		private IVerificationRepository uaCryptoRepository;

		public CryptoController()
		{
			// should be refactored after DI functionality added
			this.uaCryptoRepository =
				new UaCryptoVerificationRepository();
				//new DpaVerificationRepository();
		}

		public CryptoController(IVerificationRepository uaCryptoRepository)
		{
			this.uaCryptoRepository = uaCryptoRepository;
		}


		[HttpGet]
		public object Get(string hash)
		{
			try
			{
				return this.VerifySignature(hash);
			}
			catch (Exception exc)
			{
				throw;
			}
		}


		[HttpPost]
		public object Post(HashString hash)
		{
			try
			{
				return this.VerifySignature(hash.Hash);
			}
			catch (Exception exc)
			{
				throw;
			}
		}


		private object VerifySignature(string hash)
		{
			var xml = this.uaCryptoRepository.Verify(hash);

			var serial = xml.Descendants("Serial").SingleOrDefault();
			var code = xml.Descendants("CODE").SingleOrDefault();
			var message = xml.Descendants("MSG").SingleOrDefault();
			var result = xml.Descendants("Result").SingleOrDefault();

			return
				new
				{
					Serial = serial == null ? null : serial.Value,
					Code = code == null ? null : code.Value,
					Message = message == null ? null : message.Value,
					Result = result == null ? null : result.Value
				};
		}


		private string Base64Encode(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}
	}
}