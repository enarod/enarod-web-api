using System;
using System.Collections.Generic;
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
		public People CreatedBy { get; set; }
		public DateTime EffectiveFrom { get; set; }
		public DateTime EffectiveTo { get; set; }
		public long? Limit { get; set; }
		public string Email { get; set; }

		public int VotesCount { get; set; }

		public string Url
		{
			get
			{
				return string.Format("https://enarod.org/petition/#petition/{0}", this.ID);
			}
		}

		public Petition(Model.Petition petition)
		{
			if (petition == null) return;

			this.ID = petition.ID;
			this.Level = new PetitionLevel(petition.PetitionLevel);
			this.AddressedTo = petition.AddressedTo;
			this.Subject = petition.Subject;
			this.Category = new Entity()
							{
								ID = petition.Category.ID,
								Name = petition.Category.Name,
								Description = petition.Category.Description,
								Group = new EntityGroup(petition.Category.EntityGroup)
							};
			this.Text = petition.Text;
			this.Requirements = petition.Requirements;
			this.KeyWords = petition.KeyWords.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			this.CreatedDate = petition.CreatedDate;
			this.CreatedBy = new People(petition.Person);
			this.EffectiveFrom = petition.EffectiveFrom;
			this.EffectiveTo = petition.EffectiveTo;
			this.Limit = petition.Limit;
			this.Email = petition.Email;
		}

		public Model.Petition Map()
		{
			return new Model.Petition()
						{
							LevelID = this.Level.ID,
							AddressedTo = this.AddressedTo,
							Subject = this.Subject,
							CategoryID = this.Category.ID,
							Text = this.Text,
							Requirements = this.Requirements,
							KeyWords = this.KeyWordsAsSingleString(),
							CreatedDate = this.CreatedDate,
							CreatedBy = this.CreatedBy.ID,
							EffectiveFrom = this.EffectiveFrom,
							EffectiveTo = this.EffectiveTo,
							Limit = this.Limit,
							Email = this.Email
						};
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