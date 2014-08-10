namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class PetitionLevel : BaseEntity
	{
		public string Name { get; set; }
		public long Limit { get; set; }

		public PetitionLevel(Model.PetitionLevel level)
		{
            this.ID = level.ID;
			this.Name = level.Name;
			this.Limit = level.Limit;
		}
	}
}