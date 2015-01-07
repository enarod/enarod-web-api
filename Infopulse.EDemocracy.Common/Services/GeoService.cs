using Infopulse.EDemocracy.Common.Services.Interfaces;
using Infopulse.EDemocracy.Common.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Infopulse.EDemocracy.Common.Services
{
	public class GeoService : IGeoService
	{
		public const string GeoNamesUsername = "aregaz";
		public const string GeoNamesLanguage = "uk";

		private readonly HttpService httpService;

		public GeoService()
		{
			this.httpService = new HttpService();
		}


		public ICollection<Country> GetCountries()
		{
			var url = string.Format("http://api.geonames.org/countryInfo?username={0}&lang={1}", GeoNamesUsername, GeoNamesLanguage);
			var responseTask = this.httpService.GetStringAsync(url);
			var response = responseTask.Result;
			var countries = this.ParseResponseToCountries(response);
			return countries;
		}


		private ICollection<Country> ParseResponseToCountries(string xml)
		{
			var countries = new List<Country>();

			var xmlRoot = XDocument.Parse(xml);

			var countriesXml = xmlRoot.Descendants("country");

			foreach (var countryXml in countriesXml)
			{
				var geoNameIdXml = countryXml.Descendants("geonameId").SingleOrDefault();
				var countryCodeXml = countryXml.Descendants("countryCode").SingleOrDefault();
				var countryNameXml = countryXml.Descendants("countryName").SingleOrDefault();

				if (countryCodeXml != null && geoNameIdXml != null && countryNameXml != null)
				{
					countries.Add(new Country()
					{
						GeoNameID = int.Parse(geoNameIdXml.Value),
						Code = countryCodeXml.Value,
						Name = countryNameXml.Value
					});
				}
			}

			return countries;
		}
	}
}