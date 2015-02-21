using Infopulse.EDemocracy.Model;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionLevelRepository
	{
		IEnumerable<PetitionLevel> GetPetitionLevels();
	}
}