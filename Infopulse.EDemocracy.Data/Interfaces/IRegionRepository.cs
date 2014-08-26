using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.Common;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IRegionRepository
	{
		OperationResult<IEnumerable<Region>> GetRegions(int petitonLevelId);
	}
}