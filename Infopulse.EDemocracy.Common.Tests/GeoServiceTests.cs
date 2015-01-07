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
	}
}
