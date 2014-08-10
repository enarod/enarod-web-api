using Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.Common;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPeopleRepository
	{
		OperationResult Register(People person, Certificate cert);
	}
}