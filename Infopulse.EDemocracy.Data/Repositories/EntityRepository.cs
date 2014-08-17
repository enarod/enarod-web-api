using System.Data.Entity;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using Infopulse.EDemocracy.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Entity = Infopulse.EDemocracy.Model.BusinessEntities.Entity;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class EntityRepository : IEntityRepository
	{
		public OperationResult<IEnumerable<Entity>> GetPetitionCategories()
		{
			OperationResult<IEnumerable<Entity>> result;

			try
			{
				using (var db = new EDEntities())
				{
					var petitionCategoryEntityGroup = db.EntityGroups.SingleOrDefault(g => g.Name == "Category");
					var categories = db.Entities
						.Where(e => e.EntityGroup.ID == petitionCategoryEntityGroup.ID)
						//.Include("EntityGroup")
						.ToList();

					result = OperationResult<IEnumerable<Entity>>.Success(categories.Select(c => new Entity(c)));
				}
			}
			catch (Exception exc)
			{
				result = OperationResult<IEnumerable<Entity>>.ExceptionResult(exc);
			}

			return result;
		}
	}
}