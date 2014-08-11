using Infopulse.EDemocracy.Model.Helpers;
using System;
using System.Net.Mail;
using System.Text;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class PetitionEmailVote : BaseEntity
	{
		public Petition Petition { get; set; }
		public string Email { get; set; }
		public string Hash { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsConfirmed { get; set; }


		public PetitionEmailVote(Petition petition, string email)
		{
			this.Petition = petition;
			this.Email = email;
			this.CreatedDate = DateTime.Now;
			this.IsConfirmed = false;

			this.Hash = HashGenerator.Generate();
		}
	}
}