﻿using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using DataModels = Infopulse.EDemocracy.Model;
using WebModels = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Web
{
	public class MapperConfig
	{
		public static void Map()
		{
			MapDataToWebModels();
			MapWebToDataModels();
		}


		private static void MapDataToWebModels()
		{
			Mapper.CreateMap<DataModels.PetitionLevel, WebModels.PetitionLevel>();
			Mapper.CreateMap<DataModels.Entity, WebModels.Entity>()
				.ForMember(webModel => webModel.Group, group => group.MapFrom(dataPetition => dataPetition.EntityGroup));
			Mapper.CreateMap<DataModels.EntityGroup, WebModels.EntityGroup>();
			Mapper.CreateMap<DataModels.Person, WebModels.People>();
			Mapper.CreateMap<DataModels.PetitionVote, WebModels.PetitionVote>();
			Mapper.CreateMap<DataModels.PetitionEmailVote, WebModels.PetitionEmailVote>();
			Mapper.CreateMap<DataModels.Petition, WebModels.Petition>()
				.ForMember(webPetition => webPetition.CreatedBy, createdBy => createdBy.MapFrom(dataPetition => dataPetition.Person))
				.ForMember(webPetition => webPetition.Level, level => level.MapFrom(dataPetition => dataPetition.PetitionLevel))
				.ForMember(webPetition => webPetition.KeyWords, keyWords => keyWords.MapFrom(dataPetition => dataPetition.KeyWords.Split(',').Select(w => w.Trim())));
		}


		private static void MapWebToDataModels()
		{
			Mapper.CreateMap<WebModels.PetitionLevel, DataModels.PetitionLevel>();
			Mapper.CreateMap<WebModels.Entity, DataModels.Entity>()
				.ForMember(dataModel => dataModel.EntityGroup, group => group.MapFrom(webPetition => webPetition.Group))
				.ForMember(dataModel => dataModel.EntityGroupID, group => group.MapFrom(webPetition => webPetition.Group.ID));
			Mapper.CreateMap<WebModels.EntityGroup, DataModels.EntityGroup>();
			Mapper.CreateMap<WebModels.People, DataModels.Person>();
			Mapper.CreateMap<WebModels.PetitionVote, DataModels.PetitionVote>();
			Mapper.CreateMap<WebModels.PetitionEmailVote, DataModels.PetitionEmailVote>();
			Mapper.CreateMap<WebModels.Petition, DataModels.Petition>()
				.ForMember(dataPetition => dataPetition.Person, createdBy => createdBy.MapFrom(webPetition => webPetition.CreatedBy))
				.ForMember(dataPetition => dataPetition.CreatedBy, createdBy => createdBy.MapFrom(webPetition => webPetition.CreatedBy.ID))
				.ForMember(dataPetition => dataPetition.PetitionLevel, level => level.MapFrom(webPetition => webPetition.Level))
				.ForMember(dataPetition => dataPetition.KeyWords, keyWords => keyWords.MapFrom(webPetition => string.Join(", ", webPetition.KeyWords)))
				.ForMember(dataPetition => dataPetition.CategoryID, categoryID => categoryID.MapFrom(webPetition => webPetition.Category.ID))
				.ForMember(dataPetition => dataPetition.LevelID, levelID => levelID.MapFrom(webPetition => webPetition.Level.ID));
		}
	}
}