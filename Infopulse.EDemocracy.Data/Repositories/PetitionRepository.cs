﻿using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Text;
using Infopulse.EDemocracy.Common.Extensions;
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
				this.LoadPetitionAuthors(db, new[] { petition });

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
				this.LoadPetitionAuthors(db, petitions);

				return petitions;
			}
		}


		public IEnumerable<PetitionWithVote> Search(PetitionSearchParameters searchParameters)
		{
			var script = string.Empty;

			using (var db = new EDEntities())
			{
				db.Database.Log = s => script += s;

				var sqlParameters = new List<SqlParameter>
				{
					new SqlParameter()
					{
						SqlDbType = SqlDbType.Int,
						Direction = ParameterDirection.Input,
						ParameterName = "PetitionID",
						Value = DBNull.Value
					}
				};

				if (!searchParameters.Text.IsEmpty())
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.NVarChar,
							Direction = ParameterDirection.Input,
							ParameterName = "SearchText",
							Value = (object)searchParameters.Text
						});
				}

				if (!searchParameters.KeyWord.IsEmpty())
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.NVarChar,
							Direction = ParameterDirection.Input,
							ParameterName = "KeyWordText",
							Value = (object)searchParameters.KeyWord
						});
				}

				if (!searchParameters.Category.IsEmpty())
				{
					sqlParameters.Add(new SqlParameter()
						{
							SqlDbType = SqlDbType.NVarChar,
							Direction = ParameterDirection.Input,
							ParameterName = "Category",
							Value = (object)searchParameters.Category
						});
				}

				sqlParameters.Add(
					new SqlParameter()
					{
						SqlDbType = SqlDbType.Structured,
						Direction = ParameterDirection.Input,
						TypeName = "IntList",
						ParameterName = "CategoryIDs",
						Value = (object) ((searchParameters.CategoryID ?? new int[0]).Select(c => new { Number = c }).ToDataTable())
					});

				if (!searchParameters.Organization.IsEmpty())
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.NVarChar,
							Direction = ParameterDirection.Input,
							ParameterName = "Organization",
							Value = (object)searchParameters.Organization
						});
				}

				if (searchParameters.OrganizationID.HasValue)
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.Int,
							Direction = ParameterDirection.Input,
							ParameterName = "OrganizationID",
							Value = (object)searchParameters.OrganizationID.Value
						});
				}

				if (searchParameters.ShowActivePetitions.HasValue)
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.Bit,
							Direction = ParameterDirection.Input,
							ParameterName = "ShowActivePetitions",
							Value = (object)searchParameters.ShowActivePetitions.Value
						});
				}

				if (searchParameters.ShowInactivePetitions.HasValue)
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.Bit,
							Direction = ParameterDirection.Input,
							ParameterName = "ShowInactivePetitions",
							Value = (object)searchParameters.ShowInactivePetitions.Value
						});
				}

				if (searchParameters.SearchInPetitions.HasValue)
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.Bit,
							Direction = ParameterDirection.Input,
							ParameterName = "SearchInPetitions",
							Value = (object)searchParameters.SearchInPetitions.Value
						});
				}

				if (searchParameters.SearchInOrganizations.HasValue)
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.Bit,
							Direction = ParameterDirection.Input,
							ParameterName = "SearchInOrganizations",
							Value = (object)searchParameters.SearchInOrganizations.Value
						});
				}

				if (searchParameters.SearchInCategories.HasValue)
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.Bit,
							Direction = ParameterDirection.Input,
							ParameterName = "SearchInCategories",
							Value = (object)searchParameters.SearchInCategories.Value
						});
				}

				if (searchParameters.CreatedDateStart.HasValue)
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.DateTime2,
							Direction = ParameterDirection.Input,
							ParameterName = "CreatedDateStart",
							Value = (object)searchParameters.CreatedDateStart.Value
						});
				}

				if (searchParameters.CreatedDateEnd.HasValue)
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.DateTime2,
							Direction = ParameterDirection.Input,
							ParameterName = "CreatedDateEnd",
							Value = (object)searchParameters.CreatedDateEnd.Value
						});
				}

				if (searchParameters.FinishDateStart.HasValue)
				{
					sqlParameters.Add(
						new SqlParameter()
						{
							SqlDbType = SqlDbType.DateTime2,
							Direction = ParameterDirection.Input,
							ParameterName = "FinishDateStart",
							Value = (object)searchParameters.FinishDateStart.Value
						});
				}

				if (searchParameters.FinishDateEnd.HasValue)
				{
					sqlParameters.Add(
						new SqlParameter()
					{
						SqlDbType = SqlDbType.DateTime2,
						Direction = ParameterDirection.Input,
						ParameterName = "FinishDateEnd",
						Value = (object)searchParameters.FinishDateEnd.Value
					});
				}

				sqlParameters = this.AddDefaultSearchParameters(sqlParameters, searchParameters).ToList();

				var sql = "sp_Petition_GetAll " + this.GetSqlParametersNames(sqlParameters);
				var petitions = db.Database.SqlQuery<PetitionWithVote>(sql, sqlParameters.ToArray()).ToList();

				//this.LoadPetitionAuthors(db, petitions);
				//this.LoadPetitionOrganizations(db, petitions);

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

				// author
				if (newPetition.Issuer == null)
				{
					throw new Exception("Unable to create petition without author.");
				}

				var existedSigner = db.PetitionSigners.SingleOrDefault(ps => ps.Email == newPetition.Issuer.Email);
				if (existedSigner == null)
				{
					existedSigner = db.PetitionSigners.Add(newPetition.Issuer);
					db.SaveChanges();
				}

				petition.IssuerID = existedSigner.ID;
				petition.Issuer = null;

				var addedPetition = db.Petitions.Add(petition);
				db.SaveChanges();

				addedPetition = db.Petitions
					.Include("Issuer")
					.Include("Organization")
					.Include("Person")
					.Include("Category")
					.Include("Category.EntityGroup")
					.Include("PetitionLevel")
					.SingleOrDefault(p => p.ID == addedPetition.ID);

				return addedPetition;
			}
		}


		private void LoadPetitionAuthors(EDEntities db, IEnumerable<PetitionWithVote> petitions)
		{
			var issuerIDs = petitions
				.Select(p => p.IssuerID)
				.Distinct()
				.Select(p => new { Number = p })
				.ToDataTable();

			var sqlParameters = new[]
			{
				new SqlParameter
				{
					SqlDbType = SqlDbType.Structured,
					ParameterName = "List",
					Value = issuerIDs,
					TypeName = "IntList"
				}
			};

			var issuers = db.PetitionSigners.SqlQuery("sp_PetitionSigner_GetAuthorsPublicInfo @List", sqlParameters)
				.ToList();

			foreach (var petition in petitions)
			{
				petition.Issuer = issuers.SingleOrDefault(i => i.ID == petition.IssuerID);
			}
		}


		private void LoadPetitionOrganizations(EDEntities db, IEnumerable<PetitionWithVote> petitions)
		{
			var organizationalPetitions = petitions.Where(p => p.OrganizationID.HasValue).ToList();

			var organizationIDs = organizationalPetitions.Select(p => p.OrganizationID).Distinct();
			var organizations = db.Organizations.Where(p => organizationIDs.Contains(p.ID)).ToList();
			foreach (var petition in organizationalPetitions)
			{
				petition.Organization = organizations.SingleOrDefault(o => o.ID == petition.OrganizationID);
			}
		}


		private string GetSqlParametersNames(IEnumerable<SqlParameter> sqlParameters)
		{
			var names = new StringBuilder();

			foreach (var sqlParameter in sqlParameters)
			{
				var parameterName = sqlParameter.ParameterName[0] == '@'
					? sqlParameter.ParameterName.Substring(1)
					: sqlParameter.ParameterName;
				names.Append(string.Format("@{0}, ", parameterName));
			}

			var cutLength = ", ".Length;
			names.Remove(names.Length - cutLength, cutLength);

			return names.ToString();
		}
	}
}