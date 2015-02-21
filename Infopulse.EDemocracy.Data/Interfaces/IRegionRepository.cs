using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Model.BusinessEntities;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IRegionRepository
	{
		OperationResult<IEnumerable<Region>> GetRegions(int petitonLevelId);
	}
}