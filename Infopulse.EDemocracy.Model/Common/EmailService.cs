using System.Net;
using System.Net.Mail;

namespace Infopulse.EDemocracy.Model.Common
{
    public static class EmailService
    {
        public const string login = "EA2ALM";
        public const string password = "EA2ALM_2013";
        public const string from = "EA2ALM@gmail.com";
        public const string host = "smtp.gmail.com";


        public static bool SendEmail(MailMessage message)
        {
            message.From = new MailAddress(from);
            var smtp = new SmtpClient
            {
                Host = host,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(login, password)
            };
            try
            {
                smtp.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}