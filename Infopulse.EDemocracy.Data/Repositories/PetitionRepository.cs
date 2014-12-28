using System.Data;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class PetitionRepository : BaseRepository, IPetitionRepository
	{
		/// <summary>
		/// Get petition by ID.
		/// </summary>
		/// <param name="petitionID"></param>
		/// <returns></returns>
		public PetitionWithVote Get(int petitionID)
		{
			using (var db = new EDEntities())
			{
				var petition = db.Database
					.SqlQuery<PetitionWithVote>("sp_Petition_GetAll @PetitionID", new SqlParameter("@PetitionID", petitionID))
					.FirstOrDefault();
				
				if (petition == null) return null;
				petition.Person = db.People.SingleOrDefault(p => p.ID == petition.CreatedBy);
				petition.Organization = db.Organizations.SingleOrDefault(o => o.ID == petition.OrganizationID);
				
				if (!string.IsNullOrWhiteSpace(petition.Email))
				{
					var creatorVote = db.PetitionEmailVotes.SingleOrDefault(v => v.PetitionID == petitionID && v.Email == petition.Email);

					if (creatorVote == null)
					{
						throw new Exception("Ця петиція ще не підтверджена.");
					}
				}

				return petition;
			}
		}


		/// <summary>
		/// Get all petitions.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<PetitionWithVote> Get()
		{
			using (var db = new EDEntities())
			{
				var petitions = db.Database.SqlQuery<PetitionWithVote>("sp_Petition_GetAll").ToList();
				
				return petitions;
			}
		}


		/// <summary>
		/// Search petition by specific word.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public IEnumerable<PetitionWithVote> Search(string text)
		{
			var script = string.Empty;

			using (var db = new EDEntities())
			{
				db.Database.Log = s => script += s;
				var petitions = from p in this.GetVisiblePetitons(db)
								where p.KeyWords.ToUpper().Contains(text.ToUpper())
								   || p.Requirements.ToUpper().Contains(text.ToUpper())
								   || p.Subject.ToUpper().Contains(text.ToUpper())
								   || p.Text.ToUpper().Contains(text.ToUpper())
								select p;

				var result = petitions
					.Select(p => new PetitionWithVote(p) { VotesCount = this.CountPetitionVotes(db, p) })
					.ToList();
				return result;
			}
		}


		/// <summary>
		/// Search petition by specific tag.
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public IEnumerable<PetitionWithVote> KeyWordSearch(string tag)
		{
			var script = string.Empty;

			using (var db = new EDEntities())
			{
				db.Database.Log = s => script += s;
				var petitions = this.GetVisiblePetitons(db)
					.ToList()
					.Where(p => p.KeyWords.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Contains(tag));

				var result = petitions
					.Select(p => new PetitionWithVote(p) { VotesCount = this.CountPetitionVotes(db, p) })
					.ToList();
				return result;
			}
		}


		/// <summary>
		/// Create new petitions.
		/// </summary>
		/// <param name="newPetition"></param>
		/// <returns></returns>
		public Petition AddNewPetition(Petition newPetition)
		{
			using (var db = new EDEntities())
			{
				var script = string.Empty;
				db.Database.Log = s => script += s;

				// TODO: add petition already exists check
				var petition =
					new Petition()
					{
						Subject = newPetition.Subject,
						Text = newPetition.Text,
						Requirements = newPetition.Requirements,
						KeyWords = newPetition.KeyWords,
						EffectiveFrom = newPetition.EffectiveFrom == default(DateTime) ? DateTime.Now : newPetition.EffectiveFrom,
						EffectiveTo = newPetition.EffectiveTo == default(DateTime) ? DateTime.Now.AddDays(7) : newPetition.EffectiveTo,
						CreatedDate = DateTime.Now,
						Limit = newPetition.Limit,
						AddressedTo = newPetition.AddressedTo,
						Email = newPetition.Email
					};

				// CreatedBy
				var creator = db.People.SingleOrDefault(p => p.ID == newPetition.CreatedBy) ?? this.GetAnonymousUser(db);
				petition.CreatedBy = creator.ID;
				petition.Person = null;

				// Category
				if (newPetition.Category == null)
				{
					throw new Exception("Unable to get any category info.");
					//result = OperationResult<clientEntities.Petition>.Fail(-2, "Unable to get any category info.");
					//return result;
				}

				var petitionCategory = db.Entities.SingleOrDefault(c => c.Name == newPetition.Category.Name);
				if (petitionCategory == null)
				{
					throw new Exception(string.Format("Unknown petition category - {0}.", newPetition.Category.Name));
					//result = OperationResult<clientEntities.Petition>.Fail(-2, string.Format("Unknown petition category - {0}.", newPetition.Category.Name));
					//return result;
				}
				else
				{
					petition.CategoryID = petitionCategory.ID;
					petition.Category = null;
				}

				// Level
				if (newPetition.LevelID == default(long))
				{
					throw new Exception("Unable to get any petition level info.");
					//result = OperationResult<clientEntities.Petition>.Fail(-3, "Unable to get any petition level info.");
					//return result;
				}

				var level = db.PetitionLevels.SingleOrDefault(l => l.ID == newPetition.LevelID);
				if (level == null)
				{
					throw new Exception("Unknown petition level.");
					//result = OperationResult<clientEntities.Petition>.Fail(-3, "Unknown petition level.");
					//return result;
				}
				else
				{
					petition.LevelID = level.ID;
					petition.PetitionLevel = null;
				}

				var addedPetition = db.Petitions.Add(petition);
				db.SaveChanges();

				return addedPetition;
				//result = OperationResult<clientEntities.Petition>.Success(
				//	1,
				//	"The petition has successfully been created.",
				//	new clientEntities.Petition(addedPetition));
			}
		}


		private IQueryable<Petition> GetVisiblePetitons(EDEntities db)
		{
			// TODO: use stored procedure for getting not Petition, but another model with counted votes

			var petitions = from petition in db.Petitions
							where petition.PetitionVotes.Count(p => p.PetitionID == petition.ID)
							+ petition.PetitionEmailVotes.Count(p => p.PetitionID == petition.ID && p.IsConfirmed)
								  >= petition.Limit
							select petition;
			return petitions;
		}


		// TODO: get rid of following method. Use SP instead.
		private int CountPetitionVotes(EDEntities db, Petition petition)
		{
			var votesCount =
				db.PetitionVotes.Count(p => p.PetitionID == petition.ID)
				+ db.PetitionEmailVotes.Count(p => p.PetitionID == petition.ID && p.IsConfirmed);
			return votesCount;
		}
	}
}