using System;
using System.Security.Cryptography;
using System.Text;

namespace Infopulse.EDemocracy.Model.Helpers
{
	public static class HashGenerator
	{
		public static string Generate()
		{
			var shaHash = new HMACSHA512(Guid.NewGuid().ToByteArray());
			var hash = HashGenerator.ByteArrayToString(shaHash.ComputeHash(Guid.NewGuid().ToByteArray()));

			return hash;
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
	}
}