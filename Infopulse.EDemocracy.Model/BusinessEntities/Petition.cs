using System;
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
		public string KeyWords { get; set; }
		public DateTime CreatedDate { get; set; }
		public People CreatedBy { get; set; }
		public DateTime EffectiveFrom { get; set; }
		public DateTime EffectiveTo { get; set; }
		public long? Limit { get; set; }

		public int VotesCount { get; set; }

		public Petition(Model.Petition petition)
		{
            this.ID = petition.ID;
			this.Level = new PetitionLevel(petition.PetitionLevel);
			this.AddressedTo = petition.AddressedTo;
			this.Subject = petition.Subject;
			this.Category = new Entity(petition.Entity);
			this.Text = petition.Text;
			this.Requirements = petition.Requirements;
			this.KeyWords = petition.KeyWords;
			this.CreatedDate = petition.CreatedDate;
			this.CreatedBy = new People(petition.Person);
			this.EffectiveFrom = petition.EffectiveFrom;
			this.EffectiveTo = petition.EffectiveTo;
			this.Limit = petition.Limit;
		}

		public static Petition Get(long ID)
		{
			using (Model.EDEntities db = new Model.EDEntities())
			{
				var result = from p in db.Petitions
							 where p.ID == ID
							 select p;

				var first = result.FirstOrDefault();
				if (first == default(Model.Petition))
					throw new ApplicationException("Petition not found");

				return new Petition(first);
			}
		}

		Model.Petition Map()
		{
			return new Model.Petition()
						{
							LevelID = this.Level.ID,
							AddressedTo = this.AddressedTo,
							Subject = this.Subject,
							CategoryID = this.Category.ID,
							Text = this.Text,
							Requirements = this.Requirements,
							KeyWords = this.KeyWords,
							CreatedDate = this.CreatedDate,
							CreatedBy = this.CreatedBy.ID,
							EffectiveFrom = this.EffectiveFrom,
							EffectiveTo = this.EffectiveTo,
							Limit = this.Limit
						};
		}

		public void Save()
		{
			using (var db = new Model.EDEntities())
            {
                Save(db);
            }
		}

        public void Save(Model.EDEntities db)
        {
            var petition = this.Map();
            db.Petitions.Add(petition);
            db.SaveChanges();
            this.ID = petition.ID;
        }
	}
}