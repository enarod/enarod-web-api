using System.Linq;
using Infopulse.EDemocracy.Model;

namespace Infopulse.EDemocracy.Common.Extensions
{
	public static class EntityModelExtensions
	{
		public static string GetVoterName(this PetitionEmailVote vote, string defaultValue = "")
		{
			var voterUserDetails = vote?.Voter?.UserDetails?.SingleOrDefault();
			if (voterUserDetails == null) return defaultValue;

			var lastNameExists = !string.IsNullOrWhiteSpace(voterUserDetails.LastName);
			var firstNameExists = !string.IsNullOrWhiteSpace(voterUserDetails.FirstName);
			var middleNameExists = !string.IsNullOrWhiteSpace(voterUserDetails.MiddleName);


			var voterName = string.Format(
				"{0}{1}{2}{3}",
				lastNameExists
					? voterUserDetails.LastName
					:string.Empty,
				lastNameExists
					? " "
					: string.Empty,
				firstNameExists
					? $"{voterUserDetails.FirstName.Remove(1)}."
					: string.Empty,
				firstNameExists && middleNameExists
					? $" {voterUserDetails.MiddleName.Remove(1)}."
					: string.Empty);
			if (voterName.Replace(".", "").Replace(" ", "").Length == 0) return defaultValue;

            return voterName ?? defaultValue;
		}
	}
}