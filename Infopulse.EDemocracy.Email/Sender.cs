using System.Net.Mail;

namespace Infopulse.EDemocracy.Email
{
	public static class Sender
	{
		public const string FromEmail = "notifications@enarod.org";
		public const string DisplayName = "eNarod Notification";
		public static readonly MailAddress From = new MailAddress(FromEmail, DisplayName);
	}
}