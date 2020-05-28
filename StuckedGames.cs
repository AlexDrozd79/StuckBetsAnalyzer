using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace StuckBetsAnalyzer.StuckedGames
{

	public class StuckedGames
	{
		public static List<StuckGame> GetStuckGames(string externalProviderCode, DateTime lastBetRequestDate)
		{
			List<StuckGame> stuckGames = new List<StuckGame>();
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Scratch"].ConnectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("select PlayersGames.LastModifiedDate, GameProviderSerialNumber, ExternalSubProviderCode, PlayersGames.PlayerID, LastLoginDate, GameRealDebitAmount " +
					"from PlayersGames inner join Games on PlayersGames.GameID = games.GameID left join rptPlayers on PlayersGames.PlayerID = rptPlayers.PlayerID  " +
					"where games.ExternalProviderCode = '" + externalProviderCode + "'  and year(LastBetRequestDate) = " + lastBetRequestDate.Year + " and month(LastBetRequestDate) = " + lastBetRequestDate.Month +
					" and day(LastBetRequestDate) = " + lastBetRequestDate.Day + " order by LastBetRequestDate asc", conn);
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					stuckGames.Add(new StuckGame()
					{
						GameProviderSerialNumber = reader["GameProviderSerialNumber"].ToString(),
						LastModifiedDate = (DateTime)reader["LastModifiedDate"],
						ExternalSubProviderCode = reader["ExternalSubProviderCode"].ToString(),
						PlayerID = (int)reader["PlayerID"],
						PlayerLastLoginDate = (DateTime)reader["LastLoginDate"],
						GameRealDebitAmount = (decimal)reader["GameRealDebitAmount"]
					});
				}
				reader.Close();
			}

			return stuckGames;
		}

		public static List<GameProvider> GetGameProviders()
		{

			List<GameProvider> gameProviders = new List<GameProvider>();
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Scratch"].ConnectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("select	ExternalProviderCode, ExternalProviderName  from Externalproviders order by ExternalProviderCode", conn);
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					gameProviders.Add(new GameProvider()
					{
						ExternalProviderCode = reader["ExternalProviderCode"].ToString(),
						ExternalProviderName = reader["ExternalProviderName"].ToString()
					});
				}
				reader.Close();
			}

			return gameProviders;
		}


		public static List<GameSubProvider> GetGameSubProviders()
		{

			List<GameSubProvider> gameSubProviders = new List<GameSubProvider>();
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Scratch"].ConnectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand("select distinct ExternalSubProviderCode, ExternalProviderCode  from games", conn);
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					gameSubProviders.Add(new GameSubProvider()
					{
						ExternalProviderCode = reader["ExternalProviderCode"].ToString(),
						ExternalSubProviderCode = reader["ExternalSubProviderCode"].ToString(),
					});
				}
				reader.Close();
			}

			return gameSubProviders;
		}

		


	}

}
