using System;
using System.Collections.Generic;
using System.Linq;

namespace Infopulse.EDemocracy.Data
{
	public class DbLog
	{
		private static List<LogMessage> log;

		static DbLog()
		{
			DbLog.log = new List<LogMessage>();
		}

		public static void Add(string message)
		{
			log.Add(new LogMessage() {Message = message, Time = DateTime.Now, Trace = null});
		}

		public static void Add(string message, string trace)
		{
			log.Add(new LogMessage() { Message = message, Time = DateTime.Now, Trace = trace });
		}

		public static List<LogMessage> Log
		{
			get
			{
				return DbLog.log;
			}
		}

		public static void Clear()
		{
			DbLog.log.Clear();
		}
	}
}