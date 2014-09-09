using Infopulse.EDemocracy.Model.Helpers;
using System;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class PetitionEmailVote : BaseEntity
	{
		public Petition Petition { get; set; }
		public string Email { get; set; }
		public string Hash { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsConfirmed { get; set; }

		public string ConfirmUrl
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.Hash))
				{
					throw new ArgumentException("Hash cannot be empty.", "Hash");
				}

				return string.Format("https://enarod.org/app/petition/vote?hash={0}", this.Hash);
			}
		}
		

		public PetitionEmailVote()
		{
		}


		public PetitionEmailVote(Model.PetitionEmailVote emailVote)
		{
			this.ID = emailVote.ID;
			this.Email = emailVote.Email;
			this.Hash = emailVote.Hash;
			this.IsConfirmed = emailVote.IsConfirmed;
			this.CreatedDate = emailVote.CreatedDate;
		}
	}
}