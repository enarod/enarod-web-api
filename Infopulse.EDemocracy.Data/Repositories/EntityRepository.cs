using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using System.Collections.Generic;
using System.Linq;


namespace Infopulse.EDemocracy.Data.Repositories
{
	public class EntityRepository : BaseRepository, IEntityRepository
	{
		public IEnumerable<Entity> GetPetitionCategories()
		{
			IEnumerable<Entity> result;

			using (var db = new EDEntities())
			{
				this.AddLogging(db);

				var petitionCategoryEntityGroup = db.EntityGroups.SingleOrDefault(g => g.Name == "Category");
				result = db.Entities
					.Where(e => e.EntityGroup.ID == petitionCategoryEntityGroup.ID)
					//.Include("EntityGroup")
					.ToList();
			}

			return result;
		}
	}
}