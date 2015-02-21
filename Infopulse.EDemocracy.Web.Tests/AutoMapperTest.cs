using AutoMapper;
using Infopulse.EDemocracy.Model.ClientEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using DataModels = Infopulse.EDemocracy.Model;
using WebModels = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Web.Tests
{
	[TestClass]
	public class AutoMapperTest
	{
		// ReSharper disable InconsistentNaming
		[TestMethod]
		public void Map_PetitionLevelBothSide_Success()
		{
			Mapper.CreateMap<DataModels.PetitionLevel, WebModels.PetitionLevel>();
			var dataPetitonLevel = new DataModels.PetitionLevel()
								   {
									   ID = 2,
									   Name = "PetitionLevel2",
									   Limit = 100
								   };
			var webPetitionLevel = Mapper.Map<WebModels.PetitionLevel>(dataPetitonLevel);

			Mapper.CreateMap<WebModels.PetitionLevel, DataModels.PetitionLevel>();
			var webPetitionLevel2 = new WebModels.PetitionLevel()
									{
										ID = 3,
										Name = "PetitionLevel3",
										Limit = 20
									};
			var dataPetitionLevel2 = Mapper.Map<DataModels.PetitionLevel>(webPetitionLevel2);

			Assert.IsNotNull(webPetitionLevel);
			Assert.AreEqual(webPetitionLevel.ID, 2);
			Assert.AreEqual(webPetitionLevel.Name, "PetitionLevel2");
			Assert.AreEqual(webPetitionLevel.Limit, 100);

			Assert.IsNotNull(dataPetitionLevel2);
			Assert.AreEqual(dataPetitionLevel2.ID, 3);
			Assert.AreEqual(dataPetitionLevel2.Name, "PetitionLevel3");
			Assert.AreEqual(dataPetitionLevel2.Limit, 20);
		}


		[TestMethod]
		public void Map_PetitionDataToWeb_Success()
		{
			MapperConfig.Map();

			var dataPetition1 = new DataModels.Petition()
								{
									ID = 12,
									AddressedTo = "no one",
									CategoryID = 2,
									Category = new DataModels.Entity()
											   {
												   ID = 2,
												   Name = "Data category name",
												   Description = "Category from data model",
												   EntityGroupID = 3,
												   EntityGroup = new DataModels.EntityGroup()
																 {
																	 ID = 3,
																	 Name = "Data entity group",
																	 ParentID = null
																 }
											   },
									CreatedBy = 1,
									CreatedDate = DateTime.Now,
									EffectiveFrom = DateTime.Now,
									EffectiveTo = DateTime.Now.AddMinutes(1),
									Email = "test@data",
									KeyWords = "abc, c de, xy  z",
									LevelID = 4,
									PetitionLevel = new DataModels.PetitionLevel()
													{
														ID = 4,
														Limit = 1000,
														Name = "Thousand level"
													},
									Limit = 1100,
									Person = new DataModels.Person()
											 {
												 ID = 5,
												 Login = "anonymous"
											 },
									Requirements = "Long text",
									Subject = "Petition caption",
									Text = "Short text"
								};
			var webPetition1 = Mapper.Map<WebModels.Petition>(dataPetition1);

			Assert.AreEqual(webPetition1.VotesCount, default(int));
			
			Assert.IsNotNull(webPetition1.Category);
			Assert.AreEqual(webPetition1.Category.ID, 2);
			
			Assert.IsNotNull(webPetition1.Category.Group);
			Assert.AreEqual(webPetition1.Category.Group.ID, 3);
			
			Assert.IsNotNull(webPetition1.KeyWords);
			Assert.AreEqual(webPetition1.KeyWords.Count, 3);
			Assert.AreEqual(webPetition1.KeyWords[0], "abc");
			Assert.AreEqual(webPetition1.KeyWords[1], "c de");
			Assert.AreEqual(webPetition1.KeyWords[2], "xy  z");
			
			Assert.IsNotNull(webPetition1.Level);
			Assert.AreEqual(webPetition1.Level.ID, 4);
			
			Assert.IsNotNull(webPetition1.CreatedBy);
			Assert.AreEqual(webPetition1.CreatedBy.ID, 5);
		}


		[TestMethod]
		public void Map_PetitionWebToData_Success()
		{
			MapperConfig.Map();

			var webPetition1 = new WebModels.Petition()
			{
				ID = 12,
				AddressedTo = "no one",
				Category = new WebModels.Entity()
				{
					ID = 2,
					Name = "Data category name",
					Description = "Category from data model",
					Group = new WebModels.EntityGroup()
					{
						ID = 3,
						Name = "Data entity group",
						Parent = new WebModels.EntityGroup()
						         {
							         ID = -3,
									 Name = "Parent group",
									 Parent = null
						         }
					}
				},
				CreatedBy = new WebModels.People()
				            {
					            ID = 1,
								Login = "anonymous"
				            },
				CreatedDate = DateTime.Now,
				EffectiveFrom = DateTime.Now,
				EffectiveTo = DateTime.Now.AddMinutes(1),
				Email = "test@data",
				KeyWords = new List<string>() {"abc", "c de", "xy  z"},
				Level = new WebModels.PetitionLevel()
				{
					ID = 4,
					Limit = 1000,
					Name = "Thousand level"
				},
				Limit = 1100,
				Requirements = "Long text",
				Subject = "Petition caption",
				Text = "Short text",
				VotesCount = 10
			};
			var dataPetition1 = Mapper.Map<DataModels.Petition>(webPetition1);

			Assert.IsNotNull(dataPetition1.Category);
			Assert.AreEqual(dataPetition1.Category.ID, 2);
			Assert.AreEqual(dataPetition1.CategoryID, 2);

			Assert.IsNotNull(dataPetition1.Category.EntityGroup);
			Assert.AreEqual(dataPetition1.Category.EntityGroup.ID, 3);
			Assert.AreEqual(dataPetition1.Category.EntityGroupID, 3);

			Assert.IsNotNull(dataPetition1.KeyWords);
			Assert.AreEqual(dataPetition1.KeyWords, "abc, c de, xy  z");

			Assert.IsNotNull(dataPetition1.PetitionLevel);
			Assert.AreEqual(dataPetition1.LevelID, 4);

			Assert.IsNotNull(dataPetition1.Person);
			Assert.AreEqual(dataPetition1.Person.ID, 1);
			Assert.AreEqual(dataPetition1.CreatedBy, 1);
		}


		[TestMethod]
		public void Map_EmailVoteWebToServer_Success()
		{
			MapperConfig.Map();

			var webEmailVote = new EmailVote
			{
				ID = 42,
				Signer = new WebModels.PetitionSigner
				{
					Email = "jdoe@gmail.com",
					FirstName = "John",
					MiddleName = "S",
					LastName = "Doe",
					AddressLine1 = "App. 213",
					AddressLine2 = "13 Main str.",
					City = "New York",
					Region = "NY",
					Country = "USA",
					CreatedBy = "Unit test",
					CreatedDate = DateTime.UtcNow,
					ModifiedBy = null,
					ModifiedDate = null
				}
			};
			var dalEmailVote = Mapper.Map<Infopulse.EDemocracy.Model.PetitionEmailVote>(webEmailVote);

			Assert.IsNotNull(dalEmailVote);
			Assert.AreEqual(dalEmailVote.ID, -1);
			Assert.AreEqual(webEmailVote.ID, dalEmailVote.PetitionID);
			Assert.AreEqual(webEmailVote.Signer.Email, dalEmailVote.PetitionSigner.Email);
			Assert.AreEqual(webEmailVote.Signer.FirstName, dalEmailVote.PetitionSigner.FirstName);
			Assert.AreEqual(webEmailVote.Signer.MiddleName, dalEmailVote.PetitionSigner.MiddleName);
			Assert.AreEqual(webEmailVote.Signer.LastName, dalEmailVote.PetitionSigner.LastName);
			Assert.AreEqual(webEmailVote.Signer.AddressLine1, dalEmailVote.PetitionSigner.AddressLine1);
			Assert.AreEqual(webEmailVote.Signer.AddressLine2, dalEmailVote.PetitionSigner.AddressLine2);
			Assert.AreEqual(webEmailVote.Signer.Region, dalEmailVote.PetitionSigner.Region);
			Assert.AreEqual(webEmailVote.Signer.City, dalEmailVote.PetitionSigner.City);
			Assert.AreEqual(webEmailVote.Signer.Country, dalEmailVote.PetitionSigner.Country);
		}


		[TestMethod]
		public void Map_PetitionEmailVote_ServerToWeb_Success()
		{
			var now = DateTime.UtcNow;

			MapperConfig.Map();

			var dalPetitionEmailVote = new Infopulse.EDemocracy.Model.PetitionEmailVote()
			{
				ID = -13,
				PetitionID = -111,
				Hash = "#abcde",
				IsConfirmed = false,
				CreatedDate = now,
				PetitionSignerID = -4,
				PetitionSigner = new DataModels.PetitionSigner()
				{
					ID = -2,
					Email = "abc@example.com",
					FirstName = "",
					LastName = "",
					MiddleName = "",
					AddressLine1 = "address 1",
					AddressLine2 = "address 2",
					City = "city 2",
					Region = "region 3",
					Country = "Ukraine",
					CreatedDate = now,
					CreatedBy = "test",
					ModifiedBy = "test",
					ModifiedDate = now
				},
				Petition = new DataModels.Petition()
				{
					ID = -1,
					Subject = "Test 1",
					Text = "petition",
					Requirements = "req",
					Email = "user2@example.com",
					AddressedTo = "",
					CategoryID = -9,
					Category = new DataModels.Entity()
					{
						ID = -19,
						Name = "Good",
						EntityGroupID = -20,
						EntityGroup = new DataModels.EntityGroup()
						{
							ID = -21,
							Name = "Status",
							ParentID = null
						}
					},
					CreatedDate = now,
					CreatedBy = -111,
					Person = new DataModels.Person()
					{
						ID = -111,
						Login = "Test user"
					},
					LevelID = 1,
					PetitionLevel = new DataModels.PetitionLevel()
					{
						ID = -31,
						Name = "Level 1",
						Limit = 500
					},
					Limit = 1000,
					EffectiveFrom = now.AddDays(-1),
					EffectiveTo = now.AddDays(1),
					KeyWords = "abc, cde",
					OrganizationID = -41,
					Organization = new DataModels.Organization()
					{
						ID = -41,
						Logo = "abc",
						Name = "",
						Description = "",
						AcceptancePolicy = "",
						PreliminaryVoteCount = null,
						PreliminaryGatheringDays = null,
						PrivateDescription = "",
						GatheringDays = 10,
						VoteCount = 10,
						CreatedBy = "Test",
						CreatedDate = now,
						ModifiedBy = null,
						ModifiedDate = null
					}
				}
			};

			var webPetitionEmailVote = Mapper.Map<Infopulse.EDemocracy.Model.BusinessEntities.PetitionEmailVote>(dalPetitionEmailVote);

			Assert.IsNotNull(webPetitionEmailVote);
			Assert.AreEqual(dalPetitionEmailVote.Petition.ID, webPetitionEmailVote.Petition.ID);
			Assert.AreEqual(dalPetitionEmailVote.PetitionSigner.FirstName, webPetitionEmailVote.PetitionSigner.FirstName);
			Assert.AreEqual(dalPetitionEmailVote.Petition.Organization.ID, webPetitionEmailVote.Petition.Organization.ID);
		}

		// ReSharper restore InconsistentNaming
	}
}
