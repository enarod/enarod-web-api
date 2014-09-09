using System.Net.Mail;

namespace Infopulse.EDemocracy.Email
{
	public static class EnarodMailbox
	{
		public const string Login = "notifications@enarod.org";
		public const string Password = "notifications2014!";

		public const string Host = "smtp.zoho.com";
		public const int Port = 587;
		public const bool UseSsl = true;

		private const string DisplayName = "eNarod Notification";
		public static readonly MailAddress From = new MailAddress(Login, DisplayName);
	}
}