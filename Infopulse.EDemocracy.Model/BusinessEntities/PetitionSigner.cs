using System;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class PetitionSigner
	{
		public string Email { get; set; }
		
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string MiddleName { get; set; }

		public string AddressLine1 { get; set; }

		public string AddressLine2 { get; set; }

		public string City { get; set; }

		public string Region { get; set; }

		public string Country { get; set; }

		public string CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}