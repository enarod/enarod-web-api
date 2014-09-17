using Infopulse.EDemocracy.Model;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IEntityRepository
	{
		IEnumerable<Entity> GetPetitionCategories();
	}
}