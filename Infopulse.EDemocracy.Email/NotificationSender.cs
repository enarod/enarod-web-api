using System;
using System.IO;
using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.Common;

namespace Infopulse.EDemocracy.Email
{
	public class NotificationSender
	{
		private const string ConfirmPetitionVoteUrl =
			//"https://enarod.org/app/petition/vote?hash={0}";
			"http://localhost:52399/petition/vote?hash={0}";
		private const string EmailSubject = "Підтвердження голосування";
		private const bool IsBodyHtml = true;

		public OperationResult SendPetitionVoteConfirmation(Petition petition, PetitionEmailVote emailVote, string sendTo)
		{
			OperationResult result;

			try
			{
				var text = this.GetPetitionVoteConfirmationText(petition.Subject, emailVote.Hash);
			
				var sendingStatus = EmailService.SendEmail(sendTo, EmailSubject, text, IsBodyHtml);

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

		public string GetPetitionVoteConfirmationText(string petitionName, string hash)
		{
			var text = File.ReadAllText("EmailTemplates/PetitionVoteConfirmation.html")
				.Replace("{{PETITIONNAME}}", petitionName)
				.Replace("{{URL}}", string.Format(ConfirmPetitionVoteUrl, hash));
			return text;
		}
	}
}