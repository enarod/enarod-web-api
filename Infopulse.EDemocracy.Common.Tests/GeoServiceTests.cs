using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infopulse.EDemocracy.Common.Services;

namespace Infopulse.EDemocracy.Common.Tests
{
	[TestClass]
	public class GeoServiceTests
	{
		private GeoService geoService;

		[TestInitialize]
		public void InitializationEventAttribute()
		{
			this.geoService = new GeoService();
		}

		[TestMethod]
		public void GetCountries__Success()
		{
			var countries = this.geoService.GetCountries();

			Assert.IsNotNull(countries);
			Assert.IsTrue(countries.Count > 0);
		}


		[TestMethod]
		public void GeoService_GetAsync_HttpServiceWorks()
		{
			var url = "http://api.geonames.org/countryInfo?username=aregaz&lang=uk";
			var http = new HttpService();
			var requestTask = http.GetAsync(url);
			var result = requestTask.Result;

			Assert.IsNotNull(result.Content);
		}


		[TestMethod]
		public void GeoService_GetStringAsync_HttpServiceWorks()
		{
			var url = string.Format("http://api.geonames.org/countryInfo?username={0}&lang={1}", "aregaz", "uk");
			var http = new HttpService();
			var requestTask = http.GetStringAsync(url);
			var result = requestTask.Result;

			Assert.IsNotNull(result);
		}
	}
}
