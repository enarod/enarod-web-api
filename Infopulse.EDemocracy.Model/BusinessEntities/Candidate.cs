namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class Candidate : BaseEntity
	{
		public People Person { get; set; }
		public Entity CType { get; set; }
		public Entity Status { get; set; }
		public string Description { get; set; }

		public Candidate()
		{
		}

		public Candidate(Model.Candidate candidate)
		{
			this.ID = candidate.ID;
			this.Person = new People(candidate.Person);
			this.CType = new Entity(candidate.Entity);
			this.Status = new Entity(candidate.Entity1);
			this.Description = candidate.Description;
		}
	}
}