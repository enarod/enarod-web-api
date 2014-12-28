using Newtonsoft.Json;

namespace Infopulse.EDemocracy.Model.ClientEntities.v2
{
	public class PetitionSigner
	{
		public string Email { get; set; }
		
		[JsonProperty("signator-name")]
		public string FirstName { get; set; }

		[JsonProperty("signator-surname")]
		public string LastName { get; set; }

		[JsonProperty("signator-middle-name")]
		public string MiddleName { get; set; }

		[JsonProperty("signator-address1")]
		public string AddressLine1 { get; set; }

		[JsonProperty("ssignator-address2")]
		public string AddressLine2 { get; set; }

		[JsonProperty("signator-city")]
		public string City { get; set; }

		[JsonProperty("signator-area")]
		public string Region { get; set; }

		[JsonProperty("signator-country")]
		public string Country { get; set; }
	}
}