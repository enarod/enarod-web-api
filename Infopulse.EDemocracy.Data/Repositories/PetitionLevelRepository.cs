using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using System.Collections.Generic;
using System.Linq;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class PetitionLevelRepository : BaseRepository, IPetitionLevelRepository
	{
		public IEnumerable<PetitionLevel> GetPetitionLevels()
		{
			IEnumerable<PetitionLevel> result;

			using (var db = new EDEntities())
			{
				this.AddLogging(db);
				result = db.PetitionLevels.ToList();
			}

			return result;
		}
	}
}