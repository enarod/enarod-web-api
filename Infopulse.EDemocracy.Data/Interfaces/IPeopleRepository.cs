using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPeopleRepository
	{
		OperationResult Register(People person, Certificate cert);
	}
}