using System;
using System.Linq;
using Infopulse.EDemocracy.Common.Exceptions;
using Infopulse.EDemocracy.Data.Interfaces.v2;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.Helpers;

namespace Infopulse.EDemocracy.Data.Repositories
{
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
						throw new PetitionVoteIsNotConfirmedException(emailVote.PetitionSigner.Email);
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

				var petitionSigner = db.PetitionSigners.OrderByDescending(ps => ps.CreatedDate).FirstOrDefault(s => s.Email == emailVote.PetitionSigner.Email);
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