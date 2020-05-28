using StuckBetsAnalyzer.StuckedGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuckBetsAnalyzer.GameProviders.iSoftBet
{
	public class iSoftBetAnalyzer : IGameProvider
	{
		public List<AnalyzeResult> Analize(List<LogEntry> logEntries)
		{
			List<AnalyzeResult> results = new List<AnalyzeResult>();

			string[] serialNumbers = logEntries.Select(log => log.GameProviderSerialNumber).Distinct().ToArray();

			foreach (string serialNumber in serialNumbers)
			{
				List<LogEntry> gameLogs = logEntries.Where(log => log.GameProviderSerialNumber == serialNumber).OrderBy(log => log.CreateDate).ToList();
				results.Add(AnalyzeGame(gameLogs));
			}

			return results;
		}

		private AnalyzeResult AnalyzeGame(List<LogEntry> logEntries)
		{
			AnalyzeResult result = new AnalyzeResult();
			result.Reason = AnalyzeResult.Reasons.Unknown;
			
			int lastErrorCode = logEntries.Where(log => log.Code != 0).Select(log => log.Code).LastOrDefault();
			string lastLogMessage = logEntries.Select(log => log.Message).LastOrDefault().ToLower();

			if (logEntries.Count == 2 && lastErrorCode == 0 && lastLogMessage.Contains("isoftbet-gateway: debit response") && lastLogMessage.Contains("success"))
			{
				result.Reason = AnalyzeResult.Reasons.NoCreditCall;
			}
			else if (lastErrorCode == 0 && lastLogMessage.Contains("isoftbet-gateway: credit response") && lastLogMessage.Contains("success") && !lastLogMessage.Contains("closeround"))
			{
				result.Reason = AnalyzeResult.Reasons.MissingCloseround;
			}
			else if (lastErrorCode == 150000)
			{
				result.Reason = AnalyzeResult.Reasons.SessionError;
			}
			result.logEntries = logEntries;

			return result;
		}
	}
}
