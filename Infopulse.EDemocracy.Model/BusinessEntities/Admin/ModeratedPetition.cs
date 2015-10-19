using System;

namespace Infopulse.EDemocracy.Model.BusinessEntities.Admin
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

		/// <summary>
		/// Moderator info.
		/// </summary>
		public UserDetailInfo Approver { get; set; }

		/// <summary>
		/// Date of petition approvement.
		/// </summary>
		public DateTime? ApprovedDate { get; set; }
    }
}