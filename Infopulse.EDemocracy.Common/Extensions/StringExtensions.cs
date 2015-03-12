namespace Infopulse.EDemocracy.Common.Extensions
{
	public static class StringExtensions
	{
		public static bool IsEmpty(this string text)
		{
			return string.IsNullOrWhiteSpace(text);
		}
	}
}