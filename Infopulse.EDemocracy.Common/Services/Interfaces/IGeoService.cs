using System.Collections.Generic;
using Infopulse.EDemocracy.Common.Services.Models;

namespace Infopulse.EDemocracy.Common.Services.Interfaces
{
	public interface IGeoService
	{
		ICollection<Country> GetCountries();
	}
}