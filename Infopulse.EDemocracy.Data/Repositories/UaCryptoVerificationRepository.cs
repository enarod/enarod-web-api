using System.Xml.Linq;
using Infopulse.EDemocracy.Data.Interfaces;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class UaCryptoVerificationRepository : BaseRepository, IVerificationRepository
	{
        public XElement Verify(string hash)
        {
            var client = new Verification.UaCrypto.UACryptoWSClient();
            var response = client.verifyAttach(hash);
            return XElement.Parse(response);
        }
    }
}