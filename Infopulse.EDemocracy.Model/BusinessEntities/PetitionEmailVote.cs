using System;
using System.Configuration;

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

				var domain = ConfigurationManager.AppSettings["ServiceDomain"];
				return string.Format("{0}/petition/vote?hash={1}", domain, this.Hash);
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


		public PetitionEmailVote(Model.PetitionEmailVote emailVote, Model.Petition petition) : this(emailVote)
		{
			this.Petition = new Petition(petition);
		}
	}
}