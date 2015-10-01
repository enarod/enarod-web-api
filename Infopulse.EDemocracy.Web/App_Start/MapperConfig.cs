using AutoMapper;
using System.Linq;
using Infopulse.EDemocracy.Common.Extensions;
using Infopulse.EDemocracy.Model.ClientEntities;
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
			Mapper.CreateMap<DataModels.Organization, WebModels.Organization>();
			Mapper.CreateMap<DataModels.Entity, WebModels.Entity>()
				.ForMember(webModel => webModel.Group, group => group.MapFrom(dataPetition => dataPetition.EntityGroup));
			Mapper.CreateMap<DataModels.EntityGroup, WebModels.EntityGroup>();
			Mapper.CreateMap<DataModels.Person, WebModels.People>();
			Mapper.CreateMap<DataModels.PetitionVote, WebModels.PetitionVote>();
			Mapper.CreateMap<DataModels.PetitionEmailVote, WebModels.PetitionEmailVote>()
				.ForMember(webPetitionVote => webPetitionVote.PetitionSigner, f => f.MapFrom(dataPetitionVote => dataPetitionVote.Voter))
				.ForMember(webPetitionVote => webPetitionVote.ConfirmUrl, f => f.Ignore())
				.ForMember(webPetitionVote => webPetitionVote.PetitionSigner, f => f.MapFrom(dataPetitionVote => dataPetitionVote.Voter.UserDetails.SingleOrDefault()));
			Mapper.CreateMap<DataModels.Petition, WebModels.Petition>()
				.ForMember(webPetition => webPetition.CreatedBy, createdBy => createdBy.Ignore())
				.ForMember(webPetition => webPetition.Level, level => level.MapFrom(dataPetition => dataPetition.PetitionLevel))
				.ForMember(webPetition => webPetition.KeyWords, keyWords => keyWords.MapFrom(dataPetition => dataPetition.KeyWords.Split(',').Select(w => w.Trim())));
			//.ForMember(webPetition => webPetition.Author, f => f.MapFrom(serverPetition => serverPetition.Issuer));
			Mapper.CreateMap<DataModels.PetitionWithVote, WebModels.Petition>()
				.ForMember(webPetition => webPetition.CreatedBy, createdBy => createdBy.Ignore())
				.ForMember(webPetition => webPetition.Level, level => level.MapFrom(dataPetition => dataPetition.PetitionLevel))
				.ForMember(webPetition => webPetition.KeyWords, keyWords => keyWords.MapFrom(dataPetition => dataPetition.KeyWords.Split(',').Select(w => w.Trim())));
			Mapper.CreateMap<DataModels.UserDetail, WebModels.UserDetailInfo>()
				.ForMember(webUserInfo => webUserInfo.CreatedPetitions, field => field.Ignore())
				.ForMember(webUserInfo => webUserInfo.SignedPetitions, field => field.Ignore());
			Mapper.CreateMap<DataModels.User, WebModels.User>();
			Mapper.CreateMap<DataModels.PetitionStatus, WebModels.StatusBase>();
			Mapper.CreateMap<DataModels.PetitionEmailVote, WebModels.PetitionVoteInfo>()
				.ForMember(webPetitionVote => webPetitionVote.Name, f => f.MapFrom(dataPetitionVote =>
					dataPetitionVote.GetVoterName("Невідомий користувач")))
				.ForMember(webPetitionVote => webPetitionVote.VoteDate, f => f.MapFrom(dalPetitionVote => dalPetitionVote.CreatedDate));

		}


		private static void MapWebToDataModels()
		{
			Mapper.CreateMap<WebModels.PetitionLevel, DataModels.PetitionLevel>();
			Mapper.CreateMap<WebModels.Organization, DataModels.Organization>();
			Mapper.CreateMap<WebModels.Entity, DataModels.Entity>()
				.ForMember(dataModel => dataModel.EntityGroup, group => group.MapFrom(webPetition => webPetition.Group))
				.ForMember(dataModel => dataModel.EntityGroupID, group => group.MapFrom(webPetition => webPetition.Group.ID));
			Mapper.CreateMap<WebModels.EntityGroup, DataModels.EntityGroup>();
			Mapper.CreateMap<WebModels.People, DataModels.Person>();
			Mapper.CreateMap<WebModels.PetitionVote, DataModels.PetitionVote>();
			Mapper.CreateMap<WebModels.PetitionEmailVote, DataModels.PetitionEmailVote>();
			Mapper.CreateMap<WebModels.Petition, DataModels.Petition>()
				.ForMember(dataPetition => dataPetition.CreatedBy, createdBy => createdBy.MapFrom(webPetition => webPetition.CreatedBy.UserID))
				.ForMember(dataPetition => dataPetition.PetitionLevel, level => level.MapFrom(webPetition => webPetition.Level))
				.ForMember(dataPetition => dataPetition.KeyWords, keyWords => keyWords.MapFrom(webPetition => string.Join(", ", webPetition.KeyWords)))
				.ForMember(dataPetition => dataPetition.CategoryID, categoryID => categoryID.MapFrom(webPetition => webPetition.Category.ID))
				.ForMember(dataPetition => dataPetition.LevelID, levelID => levelID.MapFrom(webPetition => webPetition.Level.ID))
				.ForMember(dataPetition => dataPetition.OrganizationID, organizationID => organizationID.MapFrom(webPetition => webPetition.Organization == null ? (int?)null : webPetition.Organization.ID))
				.ForMember(dataPetition => dataPetition.ApprovedBy, field => field.Ignore())
				.ForMember(dataPetition => dataPetition.ApprovedDate, field => field.Ignore());
            Mapper.CreateMap<EmailVote, DataModels.PetitionEmailVote>()
				.ForMember(dalVote => dalVote.ID, field => field.UseValue(-1))
				.ForMember(dalVote => dalVote.VoterID, field => field.MapFrom(webVote => webVote.Signer.UserID))
				.ForMember(dalVote => dalVote.Voter, field => field.Ignore());
				//.ForMember(dalVote => dalVote.PetitionSignerID, field => field.UseValue(-1))
				//.ForMember(dalVote => dalVote.PetitionSigner, field => field.MapFrom(webVote => webVote.Signer));
			Mapper.CreateMap<WebModels.UserDetailInfo, DataModels.UserDetail>();
			Mapper.CreateMap<WebModels.User, DataModels.User>()
				.ForMember(dataUser => dataUser.AccessFailedCount, f => f.Ignore())
				.ForMember(dataUser => dataUser.EmailConfirmed, f => f.Ignore())
				.ForMember(dataUser => dataUser.LockoutEnabled, f => f.Ignore())
				.ForMember(dataUser => dataUser.LockoutEndDateUtc, f => f.Ignore())
				.ForMember(dataUser => dataUser.PhoneNumber, f => f.Ignore())
				.ForMember(dataUser => dataUser.PhoneNumberConfirmed, f => f.Ignore())
				.ForMember(dataUser => dataUser.SecurityStamp, f => f.Ignore())
				.ForMember(dataUser => dataUser.TwoFactorEnabled, f => f.Ignore())
				.ForMember(dataUser => dataUser.UserName, f => f.Ignore());
			Mapper.CreateMap<WebModels.StatusBase, DataModels.PetitionStatus>();
		}
	}
}