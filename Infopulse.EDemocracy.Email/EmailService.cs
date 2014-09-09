//using System.Net;
//using System.Net.Mail;

//namespace Infopulse.EDemocracy.Email
//{
//	public static class EmailService
//	{
//		public static bool Send(string sendTo, string subject, string text, bool isHtml = true)
//		{
//			var message = EmailService.CreateMessage(sendTo, subject, text, isHtml);

//			var smtpClient = new SmtpClient
//			{
//				Host = EnarodMailbox.Host,
//				Port = EnarodMailbox.Port,
//				EnableSsl = EnarodMailbox.UseSsl,
//				DeliveryMethod = SmtpDeliveryMethod.Network,
//				UseDefaultCredentials = false,
//				Credentials = new NetworkCredential(EnarodMailbox.Login, EnarodMailbox.Password)
//			};

//			try
//			{
//				smtpClient.Send(message);
//				return true;
//			}
//			catch
//			{
//				return false;
//			}
//		}


//		private static MailMessage CreateMessage(string sendTo, string subject, string text, bool isHtml)
//		{
//			var message = new MailMessage
//			{
//				Subject = subject,
//				Body = text,
//				IsBodyHtml = isHtml,
//				From = new MailAddress(Sender.FromEmail, Sender.DisplayName)
//			};
//			message.To.Add(sendTo);

//			return message;
//		}
//	}
//}