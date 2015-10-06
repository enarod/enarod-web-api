using Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Web.Areas.Admin.Models
{
	/// <summary>
	/// Petition with cpecial properties indicating moderation status.
	/// </summary>
	public class ModeratedPetition : Petition
	{
		/// <summary>
		/// Indicates whether moderator checked current petition for specific action.
		/// </summary>
		public bool IsChecked { get; set; }
	}
}