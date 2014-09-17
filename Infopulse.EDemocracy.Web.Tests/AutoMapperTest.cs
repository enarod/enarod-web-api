using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

		// ReSharper restore InconsistentNaming
	}
}
