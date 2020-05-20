using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuckBetsAnalyzer.StuckedGames
{
	public class LogEntry
	{
		public DateTime CreateDate { get; set; }

		public string BTMID { get; set; }

		public int Code { get; set; }

		public string Message { get; set; }


	}
}
