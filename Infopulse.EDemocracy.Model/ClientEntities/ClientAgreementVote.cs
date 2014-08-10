namespace Infopulse.EDemocracy.Model.ClientEntities
{
	public class ClientAgreementVote
	{
		public int AgreementID { get; set; }
		public string SignedData { get; set; }
		public string Signature { get; set; }
		public string CertificateType { get; set; }
	}
}