using Infopulse.EDemocracy.Data.Exceptions;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities;
using Infopulse.EDemocracy.Model.Common;
using Infopulse.EDemocracy.Model.Helpers;
using Infopulse.EDemocracy.Model.Resources;
using System;
using System.Linq;
using Certificate = Infopulse.EDemocracy.Model.Certificate;
using PetitionEmailVote = Infopulse.EDemocracy.Model.PetitionEmailVote;
using PetitionVote = Infopulse.EDemocracy.Model.PetitionVote;

namespace Infopulse.EDemocracy.Data.Repositories
{
	//public class PetitionVoteRepository : BaseRepository, IPetitionVoteRepository
	//{
	//	public OperationResult Vote(ClientPetitionVote petitionVote, string certificateSerialNumber)
	//	{
	//		var vote = new PetitionVote()
	//		{
	//			PetitionID = petitionVote.PetitionID,
	//			Certificate = new Certificate() { SerialNumber = certificateSerialNumber },
	//			SignedData = petitionVote.SignedData,
	//			SignedHash = petitionVote.Signature
	//		};

	//		OperationResult result;

	//		using (var db = new EDEntities())
	//		{
	//			var certificate = db.Certificates.SingleOrDefault(c => c.SerialNumber == vote.Certificate.SerialNumber);
	//			if (certificate == null)
	//			{
	//				result = OperationResult.Fail(-4, "Certificate is not registered.");
	//				return result;
	//			}

	//			vote.CertificateID = certificate.ID;
	//			vote.PersonID = certificate.PersonID;
	//			vote.CreatedDate = DateTime.Now;

	//			vote.Petition = null;
	//			vote.Certificate = null;
	//			vote.Person = null;

	//			////var votes = (from p in db.PetitionVotes
	//			////			 where p.PetitionID == vote.PetitionID
	//			////				&& (p.PersonID == vote.PersonID || p.CertificateID == vote.CertificateID || p.Certificate.SerialNumber == vote.Certificate.SerialNumber)
	//			////			 select p);
	//			//var votes = db.PetitionVotes.Where(p =>
	//			//	p.PetitionID == vote.PetitionID &&
	//			//	(p.PersonID == vote.PersonID || p.CertificateID == vote.CertificateID ||
	//			//	 p.Certificate.SerialNumber == vote.Certificate.SerialNumber));

	//			//if (votes.Any())
	//			//{
	//			//	result = OperationResult.Fail(-5, "You already voted for this petition.");
	//			//	return result;
	//			//}

	//			db.PetitionVotes.Add(vote);
	//			db.SaveChanges();

	//			result = OperationResult.Success(1, "Agreement vote has been registered.");
	//		}

	//		return result;
	//	}


	//	public OperationResult<Model.BusinessEntities.PetitionEmailVote> CreateEmailVoteRequest(EmailVote vote)
	//	{
	//		OperationResult<Model.BusinessEntities.PetitionEmailVote> emailVoteRequestResult;

	//		Func<EDEntities, OperationResult<Model.BusinessEntities.PetitionEmailVote>> procedure = (db) =>
	//		{
	//			OperationResult<Model.BusinessEntities.PetitionEmailVote> result;

	//			var emailVote = db.PetitionEmailVotes.SingleOrDefault(v => v.PetitionID == vote.ID && v.Email == vote.Email);

	//			if (emailVote != null)
	//			{
	//				var votedPetition = db.Petitions.SingleOrDefault(p => p.ID == vote.ID);
	//				result = emailVote.IsConfirmed
	//					? OperationResult<Model.BusinessEntities.PetitionEmailVote>.Fail(
	//						int.Parse(PetitionVoteOperationResult.AlreadyVotedCode),
	//						PetitionVoteOperationResult.AlreadyVotedMessage)
	//					: OperationResult<Model.BusinessEntities.PetitionEmailVote>.Success(
	//						int.Parse(PetitionVoteOperationResult.WaitingConfirmationCode),
	//						string.Format(PetitionVoteOperationResult.WaitingConfirmationMessage, emailVote.Email),
	//						new Model.BusinessEntities.PetitionEmailVote(emailVote, votedPetition));
	//				return result;
	//			}

	//			emailVote = new PetitionEmailVote
	//			{
	//				PetitionID = vote.ID,
	//				Email = vote.Email,
	//				CreatedDate = DateTime.Now,
	//				IsConfirmed = false,
	//				Hash = HashGenerator.Generate()
	//			};

	//			db.PetitionEmailVotes.Add(emailVote);
	//			db.SaveChanges();

	//			var petition = new Model.BusinessEntities.Petition(db.Petitions.SingleOrDefault(p => p.ID == emailVote.PetitionID));
	//			var clientEmailVote =
	//				new Model.BusinessEntities.PetitionEmailVote()
	//				{
	//					ID = emailVote.ID,
	//					Petition = petition,
	//					Hash = emailVote.Hash,
	//					Email = emailVote.Email,
	//					CreatedDate = emailVote.CreatedDate,
	//					IsConfirmed = emailVote.IsConfirmed
	//				};

	//			result = OperationResult<Model.BusinessEntities.PetitionEmailVote>.Success(
	//				int.Parse(PetitionVoteOperationResult.EmailVoteRequestCreatedCode),
	//				string.Format(PetitionVoteOperationResult.EmailVoteRequestCreatedMessage, emailVote.Email),
	//				clientEmailVote);

	//			return result;
	//		};

	//		emailVoteRequestResult = DbExecuter.Execute(procedure);

	//		return emailVoteRequestResult;
	//	}


	//	public OperationResult<Model.BusinessEntities.PetitionEmailVote> ConfirmEmailVoteRequest(string hash)
	//	{
	//		OperationResult<Model.BusinessEntities.PetitionEmailVote> result;

	//		try
	//		{
	//			using (var db = new EDEntities())
	//			{
	//				var emailVote = db.PetitionEmailVotes.SingleOrDefault(p => p.Hash == hash);

	//				if (emailVote == default(PetitionEmailVote))
	//				{
	//					result = OperationResult<Model.BusinessEntities.PetitionEmailVote>.Fail(-3, "Петиція не знайдена.");
	//					return result;
	//				}

	//				if (emailVote.IsConfirmed)
	//				{
	//					result = OperationResult<Model.BusinessEntities.PetitionEmailVote>.Fail(-2, "Ви вже проголосували за цю петицію.");
	//					return result;
	//				}

	//				emailVote.IsConfirmed = true;
	//				db.SaveChanges();

	//				var emailVoteBusiness = new Model.BusinessEntities.PetitionEmailVote(emailVote);
	//				emailVoteBusiness.Petition = new Model.BusinessEntities.Petition(db.Petitions.SingleOrDefault(p => p.ID == emailVote.PetitionID));
	//				result = OperationResult<Model.BusinessEntities.PetitionEmailVote>.Success(1, "Ви успішно проголосували за петицію.", emailVoteBusiness);
	//			}
	//		}
	//		catch (Exception ex)
	//		{
	//			result = OperationResult<Model.BusinessEntities.PetitionEmailVote>.Fail(-1, ex.Message);
	//		}

	//		return result;
	//	}


	//	public OperationResult<Model.BusinessEntities.Petition> GetPetition(string hash)
	//	{
	//		OperationResult<Model.BusinessEntities.Petition> result;

	//		try
	//		{
	//			using (var db = new EDEntities())
	//			{
	//				var petitionEmailVote = db.PetitionEmailVotes.SingleOrDefault(v => v.Hash == hash);
	//				if (petitionEmailVote == null)
	//				{
	//					result = OperationResult<Model.BusinessEntities.Petition>.Fail(-2, "Petition email vote for such hash not found.");
	//					return result;
	//				}

	//				var petition = db.Petitions.SingleOrDefault(p => p.ID == petitionEmailVote.PetitionID);
	//				result = OperationResult<Model.BusinessEntities.Petition>.Success(1, "Success.", new Model.BusinessEntities.Petition(petition));
	//			}
	//		}
	//		catch (Exception exception)
	//		{
	//			result = OperationResult<Model.BusinessEntities.Petition>.ExceptionResult(exception);
	//		}

	//		return result;
	//	}


	//	public OperationResult ClearVotes()
	//	{
	//		OperationResult result;

	//		try
	//		{
	//			using (var db = new EDEntities())
	//			{
	//				db.PetitionVotes.RemoveRange(db.PetitionVotes);
	//				db.PetitionEmailVotes.RemoveRange(db.PetitionEmailVotes);
	//				db.SaveChanges();
	//				result = OperationResult.Success(1, "All petition votes has beed deleted.");
	//			}
	//		}
	//		catch (Exception exception)
	//		{
	//			result = OperationResult.ExceptionResult(exception);
	//		}

	//		return result;
	//	}


	//	public OperationResult ClearVote(int petitionID)
	//	{
	//		OperationResult result;

	//		try
	//		{
	//			using (var db = new EDEntities())
	//			{
	//				db.PetitionVotes.RemoveRange(db.PetitionVotes.Where(v => v.PetitionID == petitionID));
	//				db.PetitionEmailVotes.RemoveRange(db.PetitionEmailVotes.Where(v => v.PetitionID == petitionID));
	//				db.SaveChanges();
	//				result = OperationResult.Success(1, string.Format("Petition votes for #{0} has beed deleted.", petitionID));
	//			}
	//		}
	//		catch (Exception exception)
	//		{
	//			result = OperationResult.ExceptionResult(exception);
	//		}

	//		return result;
	//	}
	//}
}

namespace Infopulse.EDemocracy.Data.Repositories.v2
{
	using Infopulse.EDemocracy.Data.Interfaces.v2;

	public class PetitionVoteRepository : BaseRepository, IPetitionVoteRepository
	{
		public PetitionEmailVote CreateEmailVoteRequest(PetitionEmailVote emailVote)
		{
			if (emailVote == null)
			{
				throw new NullReferenceException("Unable to create email vote request with null PetitionEmailVote.");
			}

			if (emailVote.PetitionSigner == null)
			{
				throw new ArgumentNullException("emailVote", @"Unable to create email vote requst with null PetitionSigner.");
			}

			using (var db = new EDEntities())
			{
				var dbEmailVote = db.PetitionEmailVotes.SingleOrDefault(v =>
					v.PetitionID == emailVote.PetitionID &&
					v.PetitionSigner.Email == emailVote.PetitionSigner.Email);

				if (dbEmailVote != null)
				{
					if (!dbEmailVote.IsConfirmed)
					{
						throw new PetitionIsNotConfirmedException();
					}

					throw new PetitionAlreadyVotedWithEmailException();
				}

				var createdDate = DateTime.UtcNow;
				var createdBy = string.IsNullOrWhiteSpace(emailVote.PetitionSigner.CreatedBy)
					? this.UnknownAppUser
					: emailVote.PetitionSigner.CreatedBy;

				emailVote.Hash = HashGenerator.Generate();
				emailVote.IsConfirmed = false;
				emailVote.CreatedDate = createdDate;

				var petitionSigner = db.PetitionSigners.SingleOrDefault(s => s.Email == emailVote.PetitionSigner.Email);
				if (petitionSigner == null)
				{
					emailVote.PetitionSigner.CreatedDate = createdDate;
					emailVote.PetitionSigner.CreatedBy = createdBy;

					emailVote.PetitionSigner = db.PetitionSigners.Add(emailVote.PetitionSigner);
					db.SaveChanges();
				}
				else
				{
					// signer already exists in DB
					emailVote.PetitionSignerID = petitionSigner.ID;
					emailVote.PetitionSigner = petitionSigner;
				}

				emailVote = db.PetitionEmailVotes.Add(emailVote);
				db.SaveChanges();

				emailVote.Petition = db.Petitions
					.Include("Category")
					.Include("Category.EntityGroup")
					.Include("PetitionLevel")
					.Include("Organization")
					.Include("Person")
					.SingleOrDefault(p => p.ID == emailVote.PetitionID);

				return emailVote;
			}

		}

		public PetitionEmailVote ConfirmEmailVoteRequest(string hash)
		{
			using (var db = new EDEntities())
			{
				var emailVote = db.PetitionEmailVotes.SingleOrDefault(p => p.Hash == hash);

				if (emailVote == default(PetitionEmailVote))
				{
					throw new PetitionNotFoundException();
				}

				if (emailVote.IsConfirmed)
				{
					throw new PetitionAlreadyVotedWithEmailException();
				}

				emailVote.IsConfirmed = true;
				db.SaveChanges();

				return emailVote;
				//var emailVoteBusiness = new Model.BusinessEntities.PetitionEmailVote(emailVote);
				//emailVoteBusiness.Petition = new Model.BusinessEntities.Petition(db.Petitions.SingleOrDefault(p => p.ID == emailVote.PetitionID));
				//result = OperationResult<Model.BusinessEntities.PetitionEmailVote>.Success(1, "Ви успішно проголосували за петицію.", emailVoteBusiness);
			}
		}

		public PetitionEmailVote CreateRecallVoteRequest(PetitionEmailVote emailVote)
		{
			throw new NotImplementedException();
		}

		public PetitionEmailVote ConfirmRecallVoteRequest(PetitionEmailVote emailVote)
		{
			throw new NotImplementedException();
		}

		public void ClearVotes()
		{
			using (var db = new EDEntities())
			{
				db.PetitionVotes.RemoveRange(db.PetitionVotes);
				db.PetitionEmailVotes.RemoveRange(db.PetitionEmailVotes);
				db.SaveChanges();
			}
		}

		public void ClearVote(int petitionID)
		{
			using (var db = new EDEntities())
			{
				db.PetitionVotes.RemoveRange(db.PetitionVotes.Where(v => v.PetitionID == petitionID));
				db.PetitionEmailVotes.RemoveRange(db.PetitionEmailVotes.Where(v => v.PetitionID == petitionID));
				db.SaveChanges();
			}
		}
	}

}