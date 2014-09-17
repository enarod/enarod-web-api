using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.Common;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionLevelRepository
	{
		IEnumerable<PetitionLevel> GetPetitionLevels();
	}
}