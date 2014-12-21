using System;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class Organization
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string PrivateDescription { get; set; }
		public string Logo { get; set; }
		public string AcceptancePolicy { get; set; }
		public int? PreliminaryVoteCount { get; set; }
		public int? PreliminaryGatheringDays { get; set; }
		public int? VoteCount { get; set; }
		public int? GatheringDays { get; set; }

		public DateTime CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string ModifiedBy { get; set; }
	}
}