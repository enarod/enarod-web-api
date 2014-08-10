using System.Xml.Linq;

namespace Infopulse.EDemocracy.Data.Interfaces
{
    public interface IVerificationRepository
    {
        XElement Verify(string hash);
    }
}