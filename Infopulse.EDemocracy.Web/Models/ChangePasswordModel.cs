namespace Infopulse.EDemocracy.Web.Models
{
	/// <summary>
	/// Model to store password changes.
	/// </summary>
	public class ChangePasswordModel
	{
		/// <summary>
		/// Current password.
		/// </summary>
		public string CurrentPassword { get; set; }

		/// <summary>
		/// New passwords. Must comply with password policy.
		/// </summary>
		public string NewPassword { get; set; }
    }
}