using System.Collections.Generic;
using System.Linq;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class DictionariesHelper : IDictionariesHelper
	{
		public IEnumerable<PetitionLevel> GetPetitionLevels()
		{
			using (var db = new EDEntities())
			{
				var levels = db.PetitionLevels.ToList();
				return levels;
			}
		}

		public IEnumerable<Entity> GetCategories()
		{
			using (var db = new EDEntities())
			{
				var categories = db.Entities
					.Include("EntityGroup")
					.Where(e => e.EntityGroup.Name == "Category").ToList();
				return categories;
			}
		}

		public IEnumerable<PetitionStatus> GetPetitonStatuses()
		{
			using (var db = new EDEntities())
			{
				var statuses = db.PetitionStatuses.ToList();
					
				return statuses;
			}
		}
	}
}