namespace Infopulse.EDemocracy.Model.ClientEntities
{
	public class EmailVote
	{
		public long ID { get; set; }
		public BusinessEntities.PetitionSignerWeb Signer { get; set; }
	}
}