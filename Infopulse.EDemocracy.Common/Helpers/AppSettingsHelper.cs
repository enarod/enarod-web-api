using System.Configuration;

namespace Infopulse.EDemocracy.Common.Helpers
{
	public static class AppSettingsHelper
	{
		private static string domain;
		private static string Domain
		{
			get
			{
				if (string.IsNullOrEmpty(domain))
				{
					AppSettingsHelper.domain = ConfigurationManager.AppSettings["AppDomain"];
				}

				return AppSettingsHelper.domain;
			}

		}
	}
}