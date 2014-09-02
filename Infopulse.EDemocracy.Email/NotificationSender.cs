using System;
using Infopulse.EDemocracy.Model.Common;

namespace Infopulse.EDemocracy.Email
{
	public class NotificationSender
	{
		public OperationResult SendPetitionVoteConfirmation(string hash, string sendTo)
		{
			OperationResult result;

			try
			{
				const string subject = "Підтвердження голосування";
				var text = string.Format(
					"<p>Для підтвердження Вашого голосу перейдіть за наступним посиланням:</p><p><a href='https://enarod.org/app/petition/vote?hash={0}'>проголосувати</a></p>", hash);
				const bool isBodyHtml = true;
				
				var sendingStatus = EmailService.SendEmail(sendTo, subject, text, isBodyHtml);

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