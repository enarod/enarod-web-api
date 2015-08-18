using System;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class UserInfo : BaseEntity
	{
		public int UserID { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string ZipCode { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string Country { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime? ModifiedDate { get; set; }

		public List<Petition> CreatedPetitions { get; set; }

		public UserInfo()
		{
			this.CreatedPetitions = new List<Petition>();
		}
	}
}
