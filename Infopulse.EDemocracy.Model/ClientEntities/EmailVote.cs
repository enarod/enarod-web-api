using WebModels = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Model.ClientEntities
{
	public class EmailVote
	{
		public long PetitionID { get; set; }
		public WebModels.UserDetailInfo Signer { get; set; }
	}
}