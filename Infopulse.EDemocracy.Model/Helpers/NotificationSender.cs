using Infopulse.EDemocracy.Model.Common;
using System;
using System.Net.Mail;
using System.Text;

namespace Infopulse.EDemocracy.Model.Helpers
{
	public class NotificationSender
	{
		public OperationResult SendPetitionVoteConfirmation(string hash, string sendTo)
		{
			OperationResult result;

			try
			{
				var text = new StringBuilder("Для підтвердження Вашого голосу необхідно натиснути наступний лінк: ");
				text.Append("https://emarod.org/app/petition/vote?hash=");
				text.Append(hash);

				var message = new MailMessage
				{
					Subject = "Petition vote confirmation",
					Body = text.ToString()
				};

				message.To.Add(sendTo);
				var sendingStatus = EmailService.SendEmail(message);

				if (sendingStatus)
				{
					result = OperationResult.Success(1, string.Format("Запит підтвердження електронної пошти {0} відправлено.", sendTo));
				}
				else
				{
					result = OperationResult.Fail(
						-2,
						string.Format("Під час надсилання запиту підтвердження електронної пошти {0} сталася помилка.", sendTo));
				}
			}
			catch (Exception exc)
			{
				result = OperationResult.ExceptionResult(exc);
			}

			return result;
		}
	}
}