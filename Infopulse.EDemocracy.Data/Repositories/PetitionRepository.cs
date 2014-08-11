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
					var petitions = from p in db.Petitions
									where p.ID == petitionID
									select p;

					var first = petitions.FirstOrDefault();
					if (first == default(Petition))
					{
						result = OperationResult<clientEntities.Petition>.Fail(-2, "Petition not found");
						return result;
					}

					var petition = new clientEntities.Petition(first)
						{
							VotesCount = first.PetitionVotes.Count + first.PetitionEmailVotes.Count
						};

					result = OperationResult<clientEntities.Petition>.Success(1, "Success", petition);
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