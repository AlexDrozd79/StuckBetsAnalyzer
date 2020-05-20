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
		


		public static List<AnalyzeResult> GetAnalyzeResults(List<StuckGame> games)
		{
			List<AnalyzeResult> results = new List<AnalyzeResult>();

			return results;
		}

		public static List<LogEntry> GetLogs(List<StuckGame> games, string externalProviderCode)
		{
			List<LogEntry> logEntries = new List<LogEntry>();
			foreach (StuckGame game in games)
			{
				string[] btmids = GetGameBTMIDs(game, externalProviderCode);

				if (btmids.Length == 0)
				{
					btmids = GetGameBTMIDs(game, null);
				}

				if (btmids.Length > 0)
				{
					logEntries.AddRange(GetGameLogEntries(btmids, game.LastModifiedDate));
				}
				else
				{
					logEntries.Add(new LogEntry() { BTMID = "Not found for game " + game.GameProviderSerialNumber, Message = "Not found" });
				}	

				onGameProcessing?.Invoke(game);
			}

			return logEntries;

		}

		private static string[] GetGameBTMIDs(StuckGame game, string externalProviderCode)
		{
			List<string> btmids = new List<string>();
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Logger"].ConnectionString))
			{
				string sql = "select BTMID  from LogEntries with (nolock)  where Message like '%" + game.GameProviderSerialNumber + "%' and CreateDate > '" + game.LastModifiedDate.AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss") + "' and CreateDate < '" + game.LastModifiedDate.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss") + "'";
				if (externalProviderCode != null && ConfigurationManager.AppSettings[externalProviderCode] != null)
				{
					sql = "select BTMID  from LogEntries with (nolock)  where Message like '%" + game.GameProviderSerialNumber + "%' and CreateDate > '" + game.LastModifiedDate.AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss") + "' and CreateDate < '" + game.LastModifiedDate.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss") + "' and Application = '" + ConfigurationManager.AppSettings[externalProviderCode] + "'";
				}

				conn.Open();
				SqlCommand cmd = new SqlCommand(sql, conn);
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					btmids.Add( reader["BTMID"].ToString());
				}
				reader.Close();
			}
			return btmids.ToArray();
		}

		private static List<LogEntry> GetGameLogEntries(string[] BTMIDs, DateTime lastModifiedDate)
		{
			List<LogEntry> logEntries = new List<LogEntry>();
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Logger"].ConnectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("select CreateDate, BTMID, Code, Message   from  LogEntries with (nolock)  where CreateDate > '" + lastModifiedDate.AddMinutes(-2).ToString("yyyy-MM-dd HH:mm:ss") + "' and CreateDate < '" + lastModifiedDate.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss") + "' and BTMID in ('" + String.Join("','", BTMIDs) + "')  order by CreateDate", conn);
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					logEntries.Add(new LogEntry { 
						BTMID = reader["BTMID"].ToString(),
						Code = (int) reader["Code"],
						CreateDate = (DateTime)reader["CreateDate"],
						Message = reader["Message"].ToString()
					});
				}
				reader.Close();
			}
			return logEntries;
		}


	}
}
