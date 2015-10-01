using System.Linq;
using Infopulse.EDemocracy.Model;
using PetitionEmailVote = Infopulse.EDemocracy.Model.PetitionEmailVote;

namespace Infopulse.EDemocracy.Common.Extensions
{
	public static class EntityModelExtensions
	{
		public static string GetVoterName(this PetitionEmailVote vote, string defaultValue = "")
		{
			var voterUserDetails = vote?.Voter?.UserDetails?.SingleOrDefault();
			if (voterUserDetails == null) return defaultValue;

			var lastName = voterUserDetails.GetLastName();
			var firstName = voterUserDetails.GetFirstName();
			var middleName = voterUserDetails.GetMiddleName();

			var lastNameExists = !string.IsNullOrWhiteSpace(lastName);
			var firstNameExists = !string.IsNullOrWhiteSpace(firstName);
			var middleNameExists = !string.IsNullOrWhiteSpace(middleName);
			
			var voterName = string.Format(
				"{0}{1}{2}{3}",
				lastNameExists ? lastName : string.Empty,
				lastNameExists ? " " : string.Empty, // space or nothing
				firstNameExists ? $"{firstName}." : string.Empty,
				firstNameExists && middleNameExists ? $" {middleName}." : string.Empty);
			if (voterName.Replace(".", "").Replace(" ", "").Length == 0) return defaultValue;

            return voterName ?? defaultValue;
		}

		private static string GetFirstName(this UserDetail userDetail)
		{
			if (string.IsNullOrWhiteSpace(userDetail.FirstName)) return string.Empty;

			return userDetail.FirstName.Length > 1
				? userDetail.FirstName.Remove(1)
				: userDetail.FirstName;
		}

		private static string GetMiddleName(this UserDetail userDetail)
		{
			if (string.IsNullOrWhiteSpace(userDetail.MiddleName)) return string.Empty;

			return userDetail.MiddleName.Length > 1
				? userDetail.MiddleName.Remove(1)
				: userDetail.MiddleName;
		}

		private static string GetLastName(this UserDetail userDetail)
		{
			if (string.IsNullOrWhiteSpace(userDetail.LastName)) return string.Empty;

			return userDetail.LastName;
		}
	}
}