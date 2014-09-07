using BE = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Model.ClientEntities
{
	/// <summary>
	/// Vote via email.
	/// </summary>
	public class EmailVote
	{
		/// <summary>
		/// Entity ID.
		/// </summary>
		public long ID { get; set; }

		/// <summary>
		/// Voter's email.
		/// </summary>
		public string Email { get; set; }
	}
}