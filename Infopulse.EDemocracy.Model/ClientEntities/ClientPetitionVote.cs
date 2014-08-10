namespace Infopulse.EDemocracy.Model.ClientEntities
{
	public class ClientPetitionVote
	{
		public int PetitionID { get; set; }
		public string SignedData { get; set; }
		public string Signature { get; set; } // data detach
		public string CertificateType { get; set; }
		public string CertificateSerialNumer { get; set; }
	}
}