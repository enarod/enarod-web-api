using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

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
		public UserInfo CreatedBy { get; set; }
		public DateTime EffectiveFrom { get; set; }
		public DateTime EffectiveTo { get; set; }
		public long? Limit { get; set; }
		public string Email { get; set; }
		public PetitionSigner Author { get; set; }

		public Organization Organization { get; set; }

		public int VotesCount { get; set; }

		public string Url
		{
			get
			{
				var domain = ConfigurationManager.AppSettings["AppDomain"];
				return string.Format("{0}/petition/#petition/{1}", domain, this.ID);
			}
		}

		public Petition()
		{
			
		}

		public string KeyWordsAsSingleString()
		{
			var keyWordsString = string.Empty;

			for (int i = 0; i < this.KeyWords.Count; i++)
			{
				keyWordsString += this.KeyWords[i].Trim();
				if (i < this.KeyWords.Count - 1)
				{
					keyWordsString += ",";
				}
			}

			return keyWordsString;
		}
	}
}