using System.Collections.Generic;
using Infopulse.EDemocracy.Model.Common;
using Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IEntityRepository
	{
		OperationResult<IEnumerable<Entity>> GetPetitionCategories();
	}
}