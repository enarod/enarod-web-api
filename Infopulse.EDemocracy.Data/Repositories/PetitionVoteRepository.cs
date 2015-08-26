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
		public PetitionEmailVote CreateEmailVoteRequest(PetitionEmailVote petitionEmailVote)
		{
			if (petitionEmailVote == null)
			{
				throw new NullReferenceException("Unable to create email vote request with null PetitionEmailVote.");
			}

			if (petitionEmailVote.VoterID == default(int))
			{
				throw new ArgumentNullException("emailVote", @"Unable to create email vote requst with null UserID.");
			}

			using (var db = new EDEntities())
			{
				var dbEmailVote = db.PetitionEmailVotes
					.Include("Voter")
					.SingleOrDefault(v =>
						v.PetitionID == petitionEmailVote.PetitionID &&
						v.VoterID == petitionEmailVote.VoterID);

				if (dbEmailVote != null)
				{
					if (!dbEmailVote.IsConfirmed)
					{
						throw new PetitionVoteIsNotConfirmedException(dbEmailVote.Voter.Email);
					}

					throw new PetitionAlreadyVotedWithEmailException();
				}

				petitionEmailVote = db.PetitionEmailVotes.Add(petitionEmailVote);
				db.SaveChanges();

				petitionEmailVote.Petition = db.Petitions
					.Include("Category")
					.Include("Category.EntityGroup")
					.Include("PetitionLevel")
					.Include("Organization")
					.Include("Author")
					.SingleOrDefault(p => p.ID == petitionEmailVote.PetitionID);

				return petitionEmailVote;
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