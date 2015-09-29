using Newtonsoft.Json;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	public class User : BaseEntity
	{
		[JsonIgnore]
		public override long ID { get; set; }
		public string Email { get; set; }
	}
}