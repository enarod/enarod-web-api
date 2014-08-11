using System.Net;
using System.Net.Mail;

namespace Infopulse.EDemocracy.Model.Helpers
{
    public static class EmailService
    {
        public static bool SendEmail(MailMessage message)
        {
	        var senderMailbox = new Mailbox();

			message.From = new MailAddress(senderMailbox.From);
            var smtpClient = new SmtpClient
            {
                Host = senderMailbox.Host,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderMailbox.Login, senderMailbox.Password)
            };

            try
            {
                smtpClient.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}