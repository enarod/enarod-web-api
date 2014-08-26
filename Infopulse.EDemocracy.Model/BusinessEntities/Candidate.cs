namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class Candidate : BaseEntity
	{
		//public People Person { get; set; }
		//public Entity CType { get; set; }
		//public Entity Status { get; set; }
		//public string Description { get; set; }
		public string Type { get; set; }
		public string Status { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }

		public string FullName
		{
			get
			{
				return string.Format("{0} {1} {2}", this.FirstName, this.MiddleName, this.LastName);
			}
		}

		public Candidate()
		{
		}

		public Candidate(Model.Candidate candidate)
		{
			this.ID = candidate.ID;
			this.Type = candidate.Type.Name;
			this.Status = candidate.Status.Name;
		}
	}
}