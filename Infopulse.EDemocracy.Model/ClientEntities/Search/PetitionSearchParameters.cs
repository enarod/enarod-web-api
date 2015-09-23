using System;

namespace Infopulse.EDemocracy.Model.ClientEntities.Search
{
	public class PetitionSearchParameters : SearchParameters
	{
		public string Text { get; set; }
		public string Category { get; set; }
		public string Organization { get; set; }
		public string KeyWord { get; set; }
		
		public int[] CategoryID { get; set; }
		public int[] PetitionStatusID { get; set; }
		public int? OrganizationID { get; set; }

		public bool? ShowActivePetitions { get; set; }
		public bool? ShowInactivePetitions { get; set; }

		public bool? SearchInPetitions { get; set; }
		public bool? SearchInOrganizations { get; set; }
		public bool? SearchInCategories { get; set; }

		public DateTime? CreatedDateStart { get; set; }
		public DateTime? CreatedDateEnd { get; set; }

		public DateTime? FinishDateStart { get; set; }
		public DateTime? FinishDateEnd { get; set; }
	}
}