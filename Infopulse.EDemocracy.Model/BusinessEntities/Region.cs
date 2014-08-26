using System.Collections.Generic;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class Region : BaseEntity
	{
		public string Name { get; set; }
		public PetitionLevel Level { get; set; }
		public List<Candidate> Candidates { get; set; }
	}
}