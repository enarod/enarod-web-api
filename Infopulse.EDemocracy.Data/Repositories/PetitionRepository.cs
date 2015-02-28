using System.Data;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Infopulse.EDemocracy.Model.ClientEntities.Search;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class PetitionRepository : BaseRepository, IPetitionRepository
	{
		public PetitionWithVote Get(int petitionID)
		{
			using (var db = new EDEntities())
			{
				var petition = db.Database.SqlQuery<PetitionWithVote>(
						"sp_Petition_GetAll @PetitionID",
						new SqlParameter("PetitionID", petitionID))
					.SingleOrDefault();

				if (petition == null) return null;
				petition.Person = db.People.SingleOrDefault(p => p.ID == petition.CreatedBy);
				petition.Organization = db.Organizations.SingleOrDefault(o => o.ID == petition.OrganizationID);

				if (!string.IsNullOrWhiteSpace(petition.Email))
				{
					var creatorVote = db.PetitionEmailVotes.SingleOrDefault(v => v.PetitionID == petitionID && v.PetitionSigner.Email == petition.Email);

					if (creatorVote == null)
					{
						throw new Exception("Ця петиція ще не підтверджена.");
					}
				}

				return petition;
			}
		}


		public IEnumerable<PetitionWithVote> Get(bool showPreliminaryPetition = false)
		{
			using (var db = new EDEntities())
			{
				var petitions = db.Database.SqlQuery<PetitionWithVote>(
					"sp_Petition_GetAll @PetitionID, @ShowPreliminaryPetitions",
						new SqlParameter("PetitionID", DBNull.Value),
						new SqlParameter("ShowPreliminaryPetitions", showPreliminaryPetition))
					.ToList();

				return petitions;
			}
		}


		public IEnumerable<PetitionWithVote> Search(PetitionSearchParameters searchParameters)
		{
			var script = string.Empty;

			using (var db = new EDEntities())
			{
				db.Database.Log = s => script += s;

				var sqlParameters = this.AddDefaultSearchParameters(new[]
				{
					new SqlParameter()
					{
						SqlDbType = SqlDbType.Int,
						Direction = ParameterDirection.Input,
						ParameterName = "PetitionID",
						Value = DBNull.Value
					},
					new SqlParameter()
					{
						SqlDbType = SqlDbType.NVarChar,
						Direction = ParameterDirection.Input,
						ParameterName = "SearchText",
						Value = (object)searchParameters.Text ?? DBNull.Value
					},
					new SqlParameter()
					{
						SqlDbType = SqlDbType.NVarChar,
						Direction = ParameterDirection.Input,
						ParameterName = "KeyWordText",
						Value = (object)searchParameters.KeyWord ?? DBNull.Value
					},
					new SqlParameter()
					{
						SqlDbType = SqlDbType.NVarChar,
						Direction = ParameterDirection.Input,
						ParameterName = "Category",
						Value = (object)searchParameters.Category ?? DBNull.Value
					},
					new SqlParameter()
					{
						SqlDbType = SqlDbType.NVarChar,
						Direction = ParameterDirection.Input,
						ParameterName = "CategoryID",
						Value = (object)searchParameters.CategoryID ?? DBNull.Value
					},
					new SqlParameter()
					{
						SqlDbType = SqlDbType.NVarChar,
						Direction = ParameterDirection.Input,
						ParameterName = "Organization",
						Value = (object)searchParameters.Organization ?? DBNull.Value
					},
					new SqlParameter()
					{
						SqlDbType = SqlDbType.NVarChar,
						Direction = ParameterDirection.Input,
						ParameterName = "OrganizationID",
						Value = (object)searchParameters.OrganizationID ?? DBNull.Value
					},
					new SqlParameter()
					{
						SqlDbType = SqlDbType.Bit,
						Direction = ParameterDirection.Input,
						ParameterName = "ShowPreliminaryPetitions",
						Value = searchParameters.ShowPreliminaryPetitions
					},
					new SqlParameter()
					{
						SqlDbType = SqlDbType.Bit,
						Direction = ParameterDirection.Input,
						ParameterName = "ShowNewPetitions",
						Value = searchParameters.ShowNewPetitions
					}
				},
				searchParameters);

				var petitions = db.Database.SqlQuery<PetitionWithVote>(
					"sp_Petition_GetAll @PetitionID, @SearchText, @KeyWordText, " +
						"@Category, @CategoryID, @Organization, @OrganizationID, " +
						"@ShowNewPetitions, @ShowPreliminaryPetitions, " +
						"@PageNumber, @PageSize, @OrderBy",
					sqlParameters)
				.ToList();

				return petitions;
			}
		}


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
				}

				var petitionCategory = db.Entities.SingleOrDefault(c => c.Name == newPetition.Category.Name);
				if (petitionCategory == null)
				{
					throw new Exception(string.Format("Unknown petition category - {0}.", newPetition.Category.Name));
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
				}

				var level = db.PetitionLevels.SingleOrDefault(l => l.ID == newPetition.LevelID);
				if (level == null)
				{
					throw new Exception("Unknown petition level.");
				}
				else
				{
					petition.LevelID = level.ID;
					petition.PetitionLevel = null;
				}

				var addedPetition = db.Petitions.Add(petition);
				db.SaveChanges();

				return addedPetition;
			}
		}
	}
}