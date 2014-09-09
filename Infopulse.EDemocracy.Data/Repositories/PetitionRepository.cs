using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Email;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using clientEntities = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class PetitionRepository : BaseRepository, IPetitionRepository
	{
		/// <summary>
		/// Get petition by ID.
		/// </summary>
		/// <param name="petitionID"></param>
		/// <returns></returns>
		public OperationResult<clientEntities.Petition> Get(int petitionID)
		{
			OperationResult<clientEntities.Petition> result;

			try
			{
				using (var db = new EDEntities())
				{
					var petition = db.Petitions.SingleOrDefault(p => p.ID == petitionID);
					if (petition == default(Petition))
					{
						result = OperationResult<clientEntities.Petition>.Fail(-2, "Petition not found");
						return result;
					}

					var clientPetition = new clientEntities.Petition(petition);
					clientPetition.VotesCount = petition.PetitionVotes.Count + petition.PetitionEmailVotes.Count;

					result = OperationResult<clientEntities.Petition>.Success(1, "Success", clientPetition);
				}
			}
			catch (Exception ex)
			{
				result = OperationResult<clientEntities.Petition>.ExceptionResult(ex);
			}

			return result;
		}


		/// <summary>
		/// Get all petitions.
		/// </summary>
		/// <returns></returns>
		public OperationResult<IEnumerable<clientEntities.Petition>> Get()
		{
			OperationResult<IEnumerable<clientEntities.Petition>> result;

			try
			{
				using (var db = new EDEntities())
				{
					var petitions = from p in db.Petitions
									select p;

					var clientPetitions = this.GetPetitionsUnderLimit(db, petitions);
					result = OperationResult<IEnumerable<clientEntities.Petition>>.Success(clientPetitions);
				}
			}
			catch (Exception exc)
			{
				result = OperationResult<IEnumerable<clientEntities.Petition>>.ExceptionResult(exc);
			}

			return result;
		}


		/// <summary>
		/// Search petition by specific word.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public OperationResult<IEnumerable<clientEntities.Petition>> Search(string text)
		{
			OperationResult<IEnumerable<clientEntities.Petition>> result;

			var script = string.Empty;

			try
			{
				using (var db = new EDEntities())
				{

					db.Database.Log = s => script += s;
					var petitions = from p in db.Petitions
									where p.KeyWords.ToUpper().Contains(text.ToUpper())
									   || p.Requirements.ToUpper().Contains(text.ToUpper())
									   || p.Subject.ToUpper().Contains(text.ToUpper())
									   || p.Text.ToUpper().Contains(text.ToUpper())
									select p;

					var clientPetitions = this.GetPetitionsUnderLimit(db, petitions);
					result = OperationResult<IEnumerable<clientEntities.Petition>>.Success(clientPetitions);
				}
			}
			catch (Exception exc)
			{
				result = OperationResult<IEnumerable<clientEntities.Petition>>.ExceptionResult(exc);
			}

			return result;
		}


		/// <summary>
		/// Search petition by specific tag.
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public OperationResult<IEnumerable<clientEntities.Petition>> KeyWordSearch(string tag)
		{
			OperationResult<IEnumerable<clientEntities.Petition>> result;

			var script = string.Empty;

			try
			{
				using (var db = new EDEntities())
				{
					db.Database.Log = s => script += s;

					// TODO: get rid of db.Petitions.ToList():
					var petitions =
						//from petition in db.Petitions
						//where petition.KeyWords.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Contains(tag)
						//select petition;
						db.Petitions.ToList().Where(p => p.KeyWords.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Contains(tag));

					var clientPetitions = this.GetPetitionsUnderLimit(db, petitions);
					result = OperationResult<IEnumerable<clientEntities.Petition>>.Success(clientPetitions);
				}
			}
			catch (Exception exc)
			{
				result = OperationResult<IEnumerable<clientEntities.Petition>>.ExceptionResult(exc);
			}

			return result;
		}


		private IEnumerable<clientEntities.Petition> GetPetitionsUnderLimit(EDEntities db, IEnumerable<Petition> petitions)
		{
			var petitionsUnderLimit = new List<clientEntities.Petition>();
			foreach (var petition in petitions)
			{
				var votesCount = db.PetitionVotes.Count(p => p.PetitionID == petition.ID) +
								 db.PetitionEmailVotes.Count(p => p.PetitionID == petition.ID);
				if (votesCount > petition.Limit)
				{
					var clientPetition = new clientEntities.Petition(petition)
										 {
											 VotesCount = votesCount
										 };
					petitionsUnderLimit.Add(clientPetition);
				}
			}

			return petitionsUnderLimit;
		}


		/// <summary>
		/// Create new petitions.
		/// </summary>
		/// <param name="newPetition"></param>
		/// <returns></returns>
		public OperationResult<clientEntities.Petition> AddNewPetition(clientEntities.Petition newPetition)
		{
			OperationResult<clientEntities.Petition> result;

			try
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
							KeyWords = newPetition.KeyWordsAsSingleString(),
							EffectiveFrom = newPetition.EffectiveFrom == default(DateTime) ? DateTime.Now : newPetition.EffectiveFrom,
							EffectiveTo = newPetition.EffectiveTo == default(DateTime) ? DateTime.Now.AddDays(7) : newPetition.EffectiveTo,
							CreatedDate = DateTime.Now,
							Limit = newPetition.Limit,
							AddressedTo = newPetition.AddressedTo,
							Email = newPetition.Email
						};

					// CreatedBy
					var creator = db.People.SingleOrDefault(p => p.Login == newPetition.CreatedBy.Login) ?? this.GetAnonymousUser(db);
					petition.CreatedBy = creator.ID;
					petition.Person = null;

					// Category
					if (newPetition.Category == null)
					{
						result = OperationResult<clientEntities.Petition>.Fail(-2, "Unable to get any category info.");
						return result;
					}

					var petitionCategory = db.Entities.SingleOrDefault(c => c.Name == newPetition.Category.Name);
					if (petitionCategory == null)
					{
						result = OperationResult<clientEntities.Petition>.Fail(-2, string.Format("Unknown petition category - {0}.", newPetition.Category.Name));
						return result;
					}
					else
					{
						petition.CategoryID = petitionCategory.ID;
						petition.Category = null;
					}

					// Level
					if (newPetition.Level == null)
					{
						result = OperationResult<clientEntities.Petition>.Fail(-3, "Unable to get any petition level info.");
						return result;
					}

					var level = db.PetitionLevels.SingleOrDefault(l => l.ID == newPetition.Level.ID);
					if (level == null)
					{
						result = OperationResult<clientEntities.Petition>.Fail(-3, "Unknown petition level.");
						return result;
					}
					else
					{
						petition.LevelID = level.ID;
						petition.PetitionLevel = null;
					}

					var addedPetition = db.Petitions.Add(petition);
					db.SaveChanges();

					result = OperationResult<clientEntities.Petition>.Success(
						1,
						"The petition has successfully been created.",
						new clientEntities.Petition(addedPetition));
				}
			}
			catch (Exception exc)
			{
				result = OperationResult<clientEntities.Petition>.ExceptionResult(exc);
			}

			return result;
		}
	}
}