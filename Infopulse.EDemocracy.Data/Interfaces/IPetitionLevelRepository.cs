using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.Common;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionLevelRepository
	{
		OperationResult<IEnumerable<PetitionLevel>> GetPetitionLevels();
	}
}