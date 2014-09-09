//using System;
//using System.IO;
//using System.Web;
//using Infopulse.EDemocracy.Model.BusinessEntities;
//using Infopulse.EDemocracy.Model.Common;

//namespace Infopulse.EDemocracy.Email
//{
//	public class NotificationSender
//	{
//		private const string ConfirmPetitionVoteUrl = "https://enarod.org/app/petition/vote?hash={0}";
		
//		private const string TemplateFolder = "~/EmailTemplates/{0}";

//		private const string EmailSubject = "Підтвердження голосування";
//		private const bool IsBodyHtml = true;


//		public OperationResult SendPetitionVoteConfirmation(Petition petition, PetitionEmailVote emailVote, string sendTo)
//		{
//			OperationResult result;

//			try
//			{
//				var text = this.GetEmailTextFromTemplate(Action.PetitionEmailVote, petition.Subject, emailVote.Hash);

//				var sendingStatus = EmailService.Send(sendTo, EmailSubject, text, IsBodyHtml);

//				if (sendingStatus)
//				{
//					result = OperationResult.Success(1, string.Format("Запит підтвердження електронної пошти {0} відправлено.", sendTo));
//				}
//				else
//				{
//					result = OperationResult.Fail(
//						-2,
//						string.Format("Під час надсилання запиту підтвердження електронної пошти {0} сталася помилка.", sendTo));
//				}
//			}
//			catch (Exception exc)
//			{
//				result = OperationResult.ExceptionResult(exc);
//			}

//			return result;
//		}


//		public OperationResult SendConfirmationRequest(Action action, string caption, string hash, string sendTo)
//		{
//			OperationResult result;

//			try
//			{
//				var text = this.GetEmailTextFromTemplate(action, caption, hash);

//				var sendingStatus = EmailService.Send(sendTo, EmailSubject, text, IsBodyHtml);

//				if (sendingStatus)
//				{
//					result = OperationResult.Success(1, string.Format("Запит підтвердження електронної пошти {0} відправлено.", sendTo));
//				}
//				else
//				{
//					result = OperationResult.Fail(
//						-2,
//						string.Format("Під час надсилання запиту підтвердження електронної пошти {0} сталася помилка.", sendTo));
//				}
//			}
//			catch (Exception exc)
//			{
//				result = OperationResult.ExceptionResult(exc);
//			}

//			return result;
//		}

		
//		public string GetEmailTextFromTemplate(Action action, string petitionName, string hash)
//		{
//			var path = this.GetTemplateFilePath(action);
//			var text = File.ReadAllText(path)
//				.Replace("{{PETITIONNAME}}", petitionName)
//				.Replace("{{URL}}", string.Format(ConfirmPetitionVoteUrl, hash));
//			return text;
//		}


//		public string GetTemplateFilePath(Action action)
//		{
//			var path = string.Empty;

//			switch (action)
//			{
//				case Action.PetitionCreation:
//					{
//						path = HttpContext.Current.Server.MapPath(string.Format(TemplateFolder, "PetitionCreateConfirmation.html"));
//						break;
//					}
//				case Action.PetitionEmailVote:
//					{
//						path = HttpContext.Current.Server.MapPath(string.Format(TemplateFolder, "PetitionVoteConfirmation.html"));
//						break;
//					}
//				default:
//					{
//						path = null;
//						break;
//					}
//			}

//			return path;
//		}
//	}
//}