﻿namespace Infopulse.EDemocracy.Model.ClientEntities.Search
{
	public class PetitionSearchParameters : SearchParameters
	{
		public string Text { get; set; }
		public string Category { get; set; }
		public string Organization { get; set; }
		public string KeyWord { get; set; }

		public int? CategoryID { get; set; }
		public int? OrganizationID { get; set; }

		public bool ShowPreliminaryPetitions { get; set; }
		public bool ShowNewPetitions { get; set; }
	}
}