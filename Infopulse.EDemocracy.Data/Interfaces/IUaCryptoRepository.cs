using System.Xml.Linq;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IUaCryptoRepository
	{
		XElement VerifySign(string hash);
	}
}