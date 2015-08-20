using System;
using System.Configuration;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class PetitionEmailVote : BaseEntity
	{
		public Petition Petition { get; set; }
		public string Hash { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsConfirmed { get; set; }

		public PetitionSignerWeb PetitionSigner { get; set; }

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
	}
}