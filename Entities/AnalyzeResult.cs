using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StuckBetsAnalyzer.StuckedGames
{
	public class AnalyzeResult
	{
		public enum Reasons
		{
			Unknown,
			NoCreditCall,
			MissingCloseround,
			SessionError
		}

		public List<LogEntry> logEntries { get; set; }

		public Reasons Reason { get; set; }
	}
}
