using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Infopulse.EDemocracy.Model.Helpers
{
	public static class HashGenerator
	{
		public static char[] chars =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
			'V', 'W', 'X', 'Y', 'Z',
			//'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
			//'v', 'w', 'x'
		};


		/// <summary>
		/// Generates 25-chars unique hash.
		/// </summary>
		/// <returns>Unique hash as string.</returns>
		public static string Generate()
		{
			var hash = string.Empty;

			var shaHash = new HMACSHA512(Guid.NewGuid().ToByteArray());
			var byteArray = HashGenerator.ByteArrayToString(shaHash.ComputeHash(Guid.NewGuid().ToByteArray()));

			foreach (var hashChar in byteArray)
			{
				hash += HashGenerator.IntToString(Convert.ToInt32(hashChar), chars);
			}
			
			return hash.Remove(25);
		}


		private static string ByteArrayToString(byte[] input)
		{
			var result = new StringBuilder();
			foreach (byte b in input)
			{
				result.Append((char)b);
			}

			return result.ToString();
		}


		private static string IntToString(int value, char[] baseChars)
		{
			string result = string.Empty;
			int targetBase = baseChars.Length;

			do
			{
				result = baseChars[value % targetBase] + result;
				value = value / targetBase;
			}
			while (value > 0);

			return result;
		}
	}
}