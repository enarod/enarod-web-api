using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities;
using Infopulse.EDemocracy.Model.Common;
using System;
using System.Linq;
using Infopulse.EDemocracy.Model.Helpers;
using businessEntities = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class PetitionVoteRepository : IPetitionVoteRepository
	{
		public OperationResult Vote(ClientPetitionVote petitionVote, string certificateSerialNumber)
		{
			var vote = new PetitionVote()
			{
				PetitionID = petitionVote.PetitionID,
				Certificate = new Certificate() { SerialNumber = certificateSerialNumber },
				SignedData = petitionVote.SignedData,
				SignedHash = petitionVote.Signature
			};

			OperationResult result;

			using (var db = new EDEntities())
			{
				var certificate = db.Certificates.SingleOrDefault(c => c.SerialNumber == vote.Certificate.SerialNumber);
				if (certificate == null)
				{
					result = OperationResult.Fail(-4, "Certificate is not registered.");
					return result;
				}

				vote.CertificateID = certificate.ID;
				vote.PersonID = certificate.PersonID;
				vote.CreatedDate = DateTime.Now;

				vote.Petition = null;
				vote.Certificate = null;
				vote.Person = null;

				////var votes = (from p in db.PetitionVotes
				////			 where p.PetitionID == vote.PetitionID
				////				&& (p.PersonID == vote.PersonID || p.CertificateID == vote.CertificateID || p.Certificate.SerialNumber == vote.Certificate.SerialNumber)
				////			 select p);
				//var votes = db.PetitionVotes.Where(p =>
				//	p.PetitionID == vote.PetitionID &&
				//	(p.PersonID == vote.PersonID || p.CertificateID == vote.CertificateID ||
				//	 p.Certificate.SerialNumber == vote.Certificate.SerialNumber));

				//if (votes.Any())
				//{
				//	result = OperationResult.Fail(-5, "You already voted for this petition.");
				//	return result;
				//}

				db.PetitionVotes.Add(vote);
				db.SaveChanges();

				result = OperationResult.Success(1, "Agreement vote has been registered.");
			}

			return result;
		}


		public OperationResult EmailVote(EmailVote vote)
		{
			OperationResult result;

			try
			{
				using (var db = new EDEntities())
				{
					var emailVote = new PetitionEmailVote
					           {
						           PatitionID = vote.ID,
								   Email = vote.Email,
								   CreatedDate = DateTime.Now,
								   IsConfirmed = false,
								   Hash = HashGenerator.Generate()
					           };

					db.PetitionEmailVotes.Add(emailVote);
					db.SaveChanges();

					var notificationSender = new NotificationSender();
					var sendingResult = notificationSender.SendPetitionVoteConfirmation(emailVote.Hash, emailVote.Email);

					if (sendingResult.IsSuccess)
					{
						result = OperationResult.Success(1, "Ваш голос чекає підтвердження електронної пошти.");
					}
					else
					{
						result = sendingResult;
					}
				}
			}
			catch (Exception exc)
			{
				result = OperationResult.ExceptionResult(exc);
			}

			return result;
		}


		/// <summary>
		/// Confirms PetitionVote by hash.
		/// </summary>
		/// <param name="hash">PetitionID-Email hash.</param>
		/// <returns>Confirmed PetitionVote.</returns>
		public OperationResult<PetitionEmailVote> ConfirmPetitionEmailVote(string hash)
		{
			OperationResult<PetitionEmailVote> result;

			try
			{
				using (var db = new EDEntities())
				{
					var emailVote = db.PetitionEmailVotes.SingleOrDefault(p => p.Hash == hash);

					if (emailVote == default(PetitionEmailVote))
					{
						result = OperationResult<PetitionEmailVote>.Fail(-3, "Петиція не знайдена.");
						return result;
					}

					if (emailVote.IsConfirmed)
					{
						result = OperationResult<PetitionEmailVote>.Fail(-2, "Ви вже проголосували за цю петицію.");
						return result;
					}

					emailVote.IsConfirmed = true;
					db.SaveChanges();

					result = OperationResult<PetitionEmailVote>.Success(1, "Ви успішно проголосували за петицію.", emailVote);
				}
			}
			catch (Exception ex)
			{
				result = OperationResult<PetitionEmailVote>.Fail(-1, ex.Message);
			}

			return result;
		}


		public OperationResult<Petition> GetPetition(string hash)
		{
			throw new System.NotImplementedException();
		}


		public OperationResult ClearVotes()
		{
			OperationResult result;

			try
			{
				using (var db = new EDEntities())
				{
					db.PetitionVotes.RemoveRange(db.PetitionVotes);
					db.SaveChanges();
					result = OperationResult.Success(1, "All petition votes has beed deleted.");
				}
			}
			catch (Exception exception)
			{
				result = OperationResult.ExceptionResult(exception);
			}

			return result;
		}


		public OperationResult ClearVote(int petitionID)
		{
			OperationResult result;

			try
			{
				using (var db = new EDEntities())
				{
					db.PetitionVotes.RemoveRange(db.PetitionVotes.Where(v => v.PetitionID == petitionID));
					db.SaveChanges();
					result = OperationResult.Success(1, string.Format("Petition votes for #{0} has beed deleted.", petitionID));
				}
			}
			catch (Exception exception)
			{
				result = OperationResult.ExceptionResult(exception);
			}

			return result;
		}
	}
}