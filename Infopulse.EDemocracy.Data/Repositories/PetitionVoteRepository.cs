using System;
using System.Collections.Generic;
using System.Linq;
using Infopulse.EDemocracy.Common.Exceptions;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.ClientEntities.Search;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class PetitionVoteRepository : BaseRepository, IPetitionVoteRepository
	{
		public PetitionEmailVote CreateEmailVoteRequest(PetitionEmailVote petitionEmailVote)
		{
			if (petitionEmailVote == null)
			{
				throw new NullReferenceException(@"Unable to create email vote request with null PetitionEmailVote.");
			}

			if (petitionEmailVote.VoterID == default(int))
			{
				throw new ArgumentNullException(nameof(petitionEmailVote.Voter), @"Unable to create email vote requst with null UserID.");
			}

			using (var db = new EDEntities())
			{
				var dbEmailVote = db.PetitionEmailVotes
					.Include("Voter")
					.Include("Petition")
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
					.Include("PetitionStatus")
					.SingleOrDefault(p => p.ID == petitionEmailVote.PetitionID);
				petitionEmailVote.Voter = db.UserDetails
					.Include("User")
					.Include("User.UserDetails")
					.SingleOrDefault(ud => ud.UserID == petitionEmailVote.VoterID)?.User;

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
				emailVote.ConfirmationDate = DateTime.UtcNow;
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


		public IEnumerable<PetitionEmailVote> GetPetitionVotes(int petitionID, SearchParameters searchParameters)
		{
			using (var db = new EDEntities())
			{
				var votes = db.PetitionEmailVotes
					.Include("Voter")
					.Include("Voter.UserDetails")
					.Where(v => v.PetitionID == petitionID && v.IsConfirmed);
				if (searchParameters != null)
				{
					if (!string.IsNullOrWhiteSpace(searchParameters.OrderBy))
					{
						var isAscending = string.Equals(
							searchParameters.OrderDirection,
							"ASC",
							StringComparison.InvariantCultureIgnoreCase);

						switch (searchParameters.OrderBy.ToLower())
						{
							case "voter":
							case "lastname":
							case "name":
								{
									votes = isAscending
										? votes.OrderBy(v => v.Voter.UserDetails.FirstOrDefault().LastName)
										: votes.OrderByDescending(v => v.Voter.UserDetails.FirstOrDefault().LastName);
									break;
								}

							case "firstname":
								{
									votes = isAscending
										? votes.OrderBy(v => v.Voter.UserDetails.FirstOrDefault().FirstName)
										: votes.OrderByDescending(v => v.Voter.UserDetails.FirstOrDefault().FirstName);
									break;
								}

							case "date":
							case "votedate":
							default:
								{
									votes = isAscending
										? votes.OrderBy(v => v.CreatedDate)
										: votes.OrderByDescending(v => v.CreatedDate);
									break;
								}
						}
					}

					if (searchParameters.PageNumber.HasValue && searchParameters.PageSize.HasValue)
					{
						votes = votes
							.Skip((searchParameters.PageNumber.Value - 1) * searchParameters.PageSize.Value)
							.Take(searchParameters.PageSize.Value);
					}
				}

				return votes.ToList();
			}
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