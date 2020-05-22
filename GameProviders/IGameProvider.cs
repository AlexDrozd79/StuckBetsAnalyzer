using StuckBetsAnalyzer.StuckedGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuckBetsAnalyzer.GameProviders
{
	public interface IGameProvider
	{
		List<AnalyzeResult> Analize(List<LogEntry> logEntries);
		
	}
}
