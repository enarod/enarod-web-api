using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class UserDetailInfo : BaseEntity
	{
		[JsonIgnore]
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
		[JsonIgnore]
		public string CreatedBy { get; set; }
		[JsonIgnore]
		public DateTime CreatedDate { get; set; }
		[JsonIgnore]
		public string ModifiedBy { get; set; }
		[JsonIgnore]
		public DateTime? ModifiedDate { get; set; }

		public User User { get; set; }
		public List<Petition> CreatedPetitions { get; set; }

		public UserDetailInfo()
		{
			this.CreatedPetitions = new List<Petition>();
		}
	}
}
