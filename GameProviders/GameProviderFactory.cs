using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuckBetsAnalyzer.GameProviders
{
	
	public class GameProviderFactory
	{
		public static IGameProvider CreateGameProvider(string externalProviderCode)
		{
			IGameProvider gameProvider = null;
			switch (externalProviderCode)
			{
				case "S":
					gameProvider = new iSoftBet.iSoftBetAnalyzer();
					break;
				
			}
			return gameProvider;
		}
	}
}
