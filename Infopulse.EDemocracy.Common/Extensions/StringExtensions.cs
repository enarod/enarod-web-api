using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infopulse.EDemocracy.Common.Extensions
{
	public static class StringExtensions
	{
		public static bool IsEmpty(this string text)
		{
			return string.IsNullOrWhiteSpace(text);
		}

		public static string ToUtf8Encoding(this string text)
		{
			byte[] bytes = Encoding.Default.GetBytes(text);
			text = Encoding.UTF8.GetString(bytes);
			return text;
		}

		public static string To1252Encoding(this string text)
		{
			byte[] bytes = Encoding.Default.GetBytes(text);
			var encoding = Encoding.GetEncoding(1252);
			text = encoding.GetString(bytes);
			return text;
		}

		public static List<string> SplitStringBySeparator(this string text, string separator = ",")
		{
			if (string.IsNullOrWhiteSpace(text)) return new List<string>();
			if (string.IsNullOrWhiteSpace(separator)) return new List<string> {text};

			return text
				.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries)
				.Select(s => s.Trim())
				.ToList();
		}
	}
}