namespace Infopulse.EDemocracy.Model.ClientEntities
{
	public class EmailVote
	{
		public long ID { get; set; }
		public BusinessEntities.PetitionSigner Signer { get; set; }
	}
}