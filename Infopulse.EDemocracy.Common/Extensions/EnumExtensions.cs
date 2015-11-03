using Infopulse.EDemocracy.Model.Enum;

namespace Infopulse.EDemocracy.Common.Extensions
{
	public static class EnumExtensions
	{
		public static string AsText(this Role role)
		{
			return role.ToString().ToLower();
		}

		public static string AsRoleText(this int roleID)
		{
			return ((Role) roleID).AsText();
		}
	}
}