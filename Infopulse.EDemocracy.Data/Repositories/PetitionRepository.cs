using Infopulse.EDemocracy.Data.Interfaces;
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


		public OperationResult<IEnumerable<clientEntities.Petition>> Get()
		{
			OperationResult<IEnumerable<clientEntities.Petition>> result;

			try
			{
				using (var db = new EDEntities())
				{
					var petitions = from p in db.Petitions
									select p;

					var list = new List<clientEntities.Petition>();
					foreach (var item in petitions)
					{
						var clientPetition = new clientEntities.Petition(item);
						clientPetition.VotesCount = item.PetitionVotes.Count + item.PetitionEmailVotes.Count;

						if (clientPetition.VotesCount >= item.Limit)
						{
							list.Add(clientPetition);
						}
					}

					result = OperationResult<IEnumerable<clientEntities.Petition>>.Success(list);
				}
			}
			catch (Exception exc)
			{
				result = OperationResult<IEnumerable<clientEntities.Petition>>.ExceptionResult(exc);
			}

			return result;
		}


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

					var list = new List<clientEntities.Petition>();
					foreach (var item in petitions)
					{
						var clientPetition = new clientEntities.Petition(item)
						{
							VotesCount = item.PetitionVotes.Count() + item.PetitionEmailVotes.Count
						};

						if (clientPetition.VotesCount >= item.Limit)
						{
							list.Add(clientPetition);
						}
					}

					result = OperationResult<IEnumerable<clientEntities.Petition>>.Success(list);
				}
			}
			catch (Exception exc)
			{
				result = OperationResult<IEnumerable<clientEntities.Petition>>.ExceptionResult(exc);
			}

			return result;
		}


		public OperationResult AddNewPetition(clientEntities.Petition newPetition)
		{
			OperationResult result;

			try
			{
				using (var db = new EDEntities())
				{
					var s = newPetition.Subject.ToUpper();
					var list = from p in db.Petitions
							   where p.Subject.ToUpper() == s
							   select p;

					if (list.Any())
						result = OperationResult.Fail(-1, "Petition already exists");
					else
					{
						newPetition.Save(db);
						result = OperationResult.Success(1, "The petition has successfully been created");
					}
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