namespace Infopulse.EDemocracy.Model
{
	public class PetitionWithVote : Petition
	{
		public int? VotesCount { get; set; }

		public PetitionWithVote()
		{
			
		}

		public PetitionWithVote(Petition petition)
		{
			this.AddressedTo = petition.AddressedTo;
			this.CategoryID = petition.CategoryID;
			this.Category = petition.Category;
			this.CreatedBy = petition.CreatedBy;
			this.Person = petition.Person;
			this.CreatedDate = petition.CreatedDate;
			this.EffectiveFrom = petition.EffectiveFrom;
			this.EffectiveTo = petition.EffectiveTo;
			this.Email = petition.Email;
			this.ID = petition.ID;
			this.KeyWords = petition.KeyWords;
			this.LevelID = petition.LevelID;
			this.PetitionLevel = petition.PetitionLevel;
			this.Limit = petition.Limit;
			this.Requirements = petition.Requirements;
			this.Text = petition.Text;
			this.Subject = petition.Subject;

			this.PetitionEmailVotes = petition.PetitionEmailVotes;
			this.PetitionVotes = petition.PetitionVotes;

			this.VotesCount = null;
		}
	}
}