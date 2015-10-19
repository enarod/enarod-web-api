namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class PetitionLevel : BaseEntity
	{
		public string Name { get; set; }
		public string Description => this.Name;
		public long Limit { get; set; }

		public PetitionLevel()
		{
		}
	}
}