using Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Model.ClientEntities
{
	public class EmailVote
	{
		public long ID { get; set; }
		public UserInfo Signer { get; set; }
	}
}