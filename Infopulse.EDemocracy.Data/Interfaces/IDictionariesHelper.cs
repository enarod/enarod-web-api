using Infopulse.EDemocracy.Model;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IDictionariesHelper
	{
		IEnumerable<PetitionLevel> GetPetitionLevels();
		IEnumerable<Entity> GetCategories();
		IEnumerable<PetitionStatus> GetPetitonStatuses();
	}
}