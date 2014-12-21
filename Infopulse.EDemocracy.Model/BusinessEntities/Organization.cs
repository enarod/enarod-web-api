using System;

namespace Infopulse.EDemocracy.Model.BusinessEntities
{
	/// <summary>
	/// Organization which can be reciever of petitions.
	/// </summary>
	public class Organization
	{
		/// <summary>
		/// Organiztion ID.
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Public official name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Long description of organization which will be displayed for everyone on petition page.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Provate description which will be visible only for admins.
		/// </summary>
		public string PrivateDescription { get; set; }

		/// <summary>
		/// Link to image-logo of organization.
		/// </summary>
		public string Logo { get; set; }

		/// <summary>
		/// Organization's petition acceptance policy.
		/// </summary>
		public string AcceptancePolicy { get; set; }

		/// <summary>
		/// Preliminary number of votes which must be gathered for petition became visible on main page.
		/// </summary>
		public int? PreliminaryVoteCount { get; set; }

		/// <summary>
		/// Preliminary number of days while petition preliminary gethering will be active.
		/// </summary>
		public int? PreliminaryGatheringDays { get; set; }

		/// <summary>
		/// Vote number required to send petition to recipient organization.
		/// </summary>
		public int? VoteCount { get; set; }

		/// <summary>
		/// Days number while petition gathereing will be active.
		/// </summary>
		public int? GatheringDays { get; set; }

		/// <summary>
		/// Date of Organization been registered in DB.
		/// </summary>
		/// <remarks>
		/// Do not send on create.
		/// </remarks>
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// Describes person who added an organisation.
		/// </summary>
		/// <remarks>
		/// Free form - there are no constraints on this field. It can be enything - email, full name or empty.
		/// </remarks>
		public string CreatedBy { get; set; }

		/// <summary>
		/// Last update date.
		/// </summary>
		public DateTime? ModifiedDate { get; set; }

		/// <summary>
		/// Last time updated by.
		/// </summary>
		/// <remarks>
		/// Free form - there are no constraints on this field. It can be enything - email, full name or empty.
		/// </remarks>
		public string ModifiedBy { get; set; }
	}
}