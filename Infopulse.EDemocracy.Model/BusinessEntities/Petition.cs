using System;
using System.Collections.Generic;
using System.Configuration;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class Petition : BaseEntity
	{
		public PetitionLevel Level { get; set; }
		public string AddressedTo { get; set; }
		public string Subject { get; set; }
		public Entity Category { get; set; }
		public string Text { get; set; }
		public string Requirements { get; set; }
		public List<string> KeyWords { get; set; }
		public DateTime CreatedDate { get; set; }
		public UserDetailInfo CreatedBy { get; set; }
		public DateTime EffectiveFrom { get; set; }
		public DateTime EffectiveTo { get; set; }
		public long? Limit { get; set; }
		public string Email { get; set; }

		public Organization Organization { get; set; }

		public int VotesCount { get; set; }

		public string Url
		{
			get
			{
				return $"{Petition.Domain}/petition/#petition/{this.ID}";
			}
		}

		public Petition()
		{
			
		}

		// TODO: following is code duplicate from Infopulse.EDemocracy.Common.Helpers.AppSettingsHelper.Domain
		// but Infopulse.EDemocracy.Model cannon reference Infopulse.EDemocracy.Common because of circular dependency
		private static string domain;
		private static string Domain
		{
			get
			{
				if (string.IsNullOrEmpty(domain))
				{
					Petition.domain = ConfigurationManager.AppSettings["AppDomain"];
				}

				return Petition.domain;
			}
			
		}
	}
}