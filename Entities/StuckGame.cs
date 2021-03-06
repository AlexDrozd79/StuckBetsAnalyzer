﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuckBetsAnalyzer.StuckedGames
{
	public class StuckGame
	{
		public DateTime LastModifiedDate { get; set; }

		public string GameProviderSerialNumber { get; set; }

		public string ExternalSubProviderCode { get; set; }

		public int PlayerID { get; set; }

		public DateTime PlayerLastLoginDate { get; set; }

		public decimal GameRealDebitAmount { get; set; }
	}
}
