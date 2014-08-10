using System.Xml.Linq;
using Infopulse.EDemocracy.Data.Interfaces;

namespace Infopulse.EDemocracy.Data.Repositories
{
    public class DpaVerificationRepository : BaseRepository, IVerificationRepository
    {
        public XElement Verify(string hash)
        {
            var client = new Verification.Dpa.UACryptoWSClient();
            var response = client.verifyAttach(hash);
            return XElement.Parse(response);
        }
    }
}