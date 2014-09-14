using System;
using System.Net;
using System.Net.Mail;
using Infopulse.EDemocracy.Email.Notifications;
using Infopulse.EDemocracy.Model.Common;

namespace Infopulse.EDemocracy.Email
{
	public static class NotificationService
	{
		private static SmtpClient Client { get; set; }

		static NotificationService()
		{
			NotificationService.Client = new SmtpClient
			{
				Host = EnarodMailbox.Host,
				Port = EnarodMailbox.Port,
				EnableSsl = EnarodMailbox.UseSsl,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(EnarodMailbox.Login, EnarodMailbox.Password)
			};
		}


		public static bool Send(MailMessage message)
		{
			try
			{
				NotificationService.Client.Send(message);
				return true;
			}
			catch
			{
				return false;
			}
		}


		public static OperationResult Send(NotificationBase notification)
		{
			OperationResult result;

			var message = NotificationService.CreateMessage(notification);

			try
			{
				NotificationService.Client.Send(message);

				result = OperationResult.Success(11, notification.SuccessfulySentMessage);
			}
			catch (Exception exception)
			{
				result = OperationResult.ExceptionResult(exception);
			}

			return result;
		}


		private static MailMessage CreateMessage(NotificationBase notification)
		{
			var message = new MailMessage
			{
				Subject = notification.Subject,
				Body = notification.Text,
				IsBodyHtml = true,
				From = EnarodMailbox.From
			};
			message.To.Add(notification.Recipient);

			return message;
		}
	}
}