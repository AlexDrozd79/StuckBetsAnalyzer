using StuckBetsAnalyzer.GameProviders;
using StuckBetsAnalyzer.StuckedGames;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StuckBetsAnalyzer
{
	public class StuckGamesLogAnalyzer
	{
		public delegate void GamePorcessingDelegate(StuckGame game);

		public delegate void ProcessingFinishgDelegate(List<LogEntry> logEntries);

		public static event GamePorcessingDelegate onGameProcessing; 
		


		public static List<LogEntry> GetLogs(List<StuckGame> games, string externalProviderCode)
		{
			List<LogEntry> logEntries = new List<LogEntry>();
			foreach (StuckGame game in games)
			{
				string[] btmids = GetGameBTMIDs(game, externalProviderCode);

				if (btmids.Length == 0 && !bool.Parse(ConfigurationManager.AppSettings["IgnoreWithoutBTMID"]))
				{
					btmids = GetGameBTMIDs(game, null);
				}

				if (btmids.Length > 0)
				{
					logEntries.AddRange(GetGameLogEntries(btmids, game));
				}
				else
				{
					logEntries.Add(new LogEntry() { BTMID = "Not found for game " + game.GameProviderSerialNumber, Message = "Not found" });
				}	

				onGameProcessing?.Invoke(game);
			}

			return logEntries;

		}


		public static string GenerateHTMLReport(List<LogEntry> logs, string externalProviderCode)
		{
			IGameProvider gameProvider = GameProviderFactory.CreateGameProvider(externalProviderCode);
			List<AnalyzeResult> analyzeResults = gameProvider.Analize(logs);
			return GenerateHTML(analyzeResults);
		}

		private static string GenerateHTML(List<AnalyzeResult> analyzeResults)
		{
			string htmlReport = string.Empty;
			
			htmlReport = Utils.HTMLHelper.StartHeadAndBody(htmlReport);
			
			htmlReport = Utils.HTMLHelper.StartTable(htmlReport);
			htmlReport = Utils.HTMLHelper.StartRow(htmlReport, "");
			htmlReport = Utils.HTMLHelper.AddCell(htmlReport, "Row");
			htmlReport = Utils.HTMLHelper.AddCell(htmlReport, "Reason");
			htmlReport = Utils.HTMLHelper.AddCell(htmlReport, "Date and time");
			htmlReport = Utils.HTMLHelper.AddCell(htmlReport, "GameProviderSerialNumber");
			htmlReport = Utils.HTMLHelper.AddCell(htmlReport, "ExternalSubProviderCode");
			htmlReport = Utils.HTMLHelper.AddCell(htmlReport, "Log Message");
			htmlReport = Utils.HTMLHelper.EndRow(htmlReport);

			for (int i = 0; i < analyzeResults.Count; i++)
			{
				AnalyzeResult result = analyzeResults[i];
				LogEntry lastLog = result.logEntries.Last();
				htmlReport = Utils.HTMLHelper.StartRow(htmlReport, result.Reason.ToString());
				htmlReport = Utils.HTMLHelper.AddCell(htmlReport, i.ToString());
				htmlReport = Utils.HTMLHelper.AddCell(htmlReport, result.Reason.ToString());
				htmlReport = Utils.HTMLHelper.AddCell(htmlReport, lastLog.CreateDate.ToString("yyyy-MM-dd HH:mm:ss")); 
				htmlReport = Utils.HTMLHelper.AddCell(htmlReport, lastLog.GameProviderSerialNumber);    
				htmlReport = Utils.HTMLHelper.AddCell(htmlReport, lastLog.ExternalSubProviderCode); 
				htmlReport = Utils.HTMLHelper.AddCell(htmlReport, lastLog.Message);  
				htmlReport = Utils.HTMLHelper.EndRow(htmlReport);
			}

			htmlReport = Utils.HTMLHelper.EndTable(htmlReport);
			htmlReport = Utils.HTMLHelper.AddBR(htmlReport);

			Dictionary<string, int> groupedData = analyzeResults.GroupBy(log => log.Reason.ToString()).Select(gr => new { gr.Key, value = gr.Count() }).ToDictionary(a => a.Key, b => b.value);

			htmlReport = Utils.HTMLHelper.StartTable(htmlReport);
			foreach (KeyValuePair<string, int> item in groupedData)
			{
				htmlReport = Utils.HTMLHelper.StartRow(htmlReport, item.Key);
				htmlReport = Utils.HTMLHelper.AddCell(htmlReport, item.Key);
				htmlReport = Utils.HTMLHelper.AddCell(htmlReport, item.Value.ToString());
				htmlReport = Utils.HTMLHelper.EndRow(htmlReport);
			}
			htmlReport = Utils.HTMLHelper.EndTable(htmlReport);

			htmlReport = Utils.HTMLHelper.EndBody(htmlReport);
			return htmlReport;
		}


		private static string[] GetGameBTMIDs(StuckGame game, string externalProviderCode)
		{
			List<string> btmids = new List<string>();
			try
			{
				using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Logger"].ConnectionString))
				{
					string sql = "select BTMID  from LogEntries with (nolock)  where Message like '%" + game.GameProviderSerialNumber + "%' and CreateDate > '" + game.LastModifiedDate.AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss") + "' and CreateDate < '" + game.LastModifiedDate.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss") + "'";
					if (externalProviderCode != null && ConfigurationManager.AppSettings[externalProviderCode] != null)
					{
						sql = "select BTMID  from LogEntries with (nolock)  where Message like '%" + game.GameProviderSerialNumber + "%' and CreateDate > '" + game.LastModifiedDate.AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss") + "' and CreateDate < '" + game.LastModifiedDate.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss") + "' and Application = '" + ConfigurationManager.AppSettings[externalProviderCode] + "'";
					}

					conn.Open();
					SqlCommand cmd = new SqlCommand(sql, conn);
					cmd.CommandTimeout = 0;
					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						btmids.Add(reader["BTMID"].ToString());
					}
					reader.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			return btmids.ToArray();
		}

		private static List<LogEntry> GetGameLogEntries(string[] BTMIDs, StuckGame game)
		{
			List<LogEntry> logEntries = new List<LogEntry>();
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Logger"].ConnectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("select CreateDate, BTMID, Code, Message   from  LogEntries with (nolock)  where CreateDate > '" + game.LastModifiedDate.AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss") + "' and CreateDate < '" + game.LastModifiedDate.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss") + "' and BTMID in ('" + String.Join("','", BTMIDs) + "')  order by CreateDate", conn);
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					logEntries.Add(new LogEntry { 
						BTMID = reader["BTMID"].ToString(),
						Code = (int) reader["Code"],
						CreateDate = (DateTime)reader["CreateDate"],
						Message = reader["Message"].ToString(),
						GameProviderSerialNumber = game.GameProviderSerialNumber,
						ExternalSubProviderCode = game.ExternalSubProviderCode
					});
				}
				reader.Close();
			}
			return logEntries;
		}


	}
}
