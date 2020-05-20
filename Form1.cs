using StuckBetsAnalyzer.StuckedGames;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace StuckBetsAnalyzer
{
	public partial class Form1 : Form
	{

		private static List<GameSubProvider> subProviders = null;
		private static List<StuckGame> games = null;
		private Thread workreThread = null;
		private static bool inProcess = false;
		private static string currentExternalGameProvider = null;
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			StuckGamesLogAnalyzer.onGameProcessing += StuckGamesLogAnalyzer_onGameProcessing;

			comboGameProviders.DataSource = StuckedGames.StuckedGames.GetGameProviders();
			comboGameProviders.DisplayMember = "ExternalProviderName";
			comboGameProviders.ValueMember = "ExternalProviderCode";

			comboGameProviders.SelectedValue = "S";
			currentExternalGameProvider = comboGameProviders.SelectedValue.ToString();

			subProviders = StuckedGames.StuckedGames.GetGameSubProviders();

			comboGameSubProviders.DataSource = subProviders.Where(s => s.ExternalProviderCode == currentExternalGameProvider).ToList();
			comboGameSubProviders.DisplayMember = "ExternalSubProviderCode";
			comboGameSubProviders.ValueMember = "ExternalSubProviderCode";
			comboGameProviders.SelectedIndexChanged += ComboGameProviders_SelectedIndexChanged;
		}

		

		private void ComboGameProviders_SelectedIndexChanged(object sender, EventArgs e)
		{
			currentExternalGameProvider = comboGameProviders.SelectedValue.ToString();
			comboGameSubProviders.DataSource = subProviders.Where(s => s.ExternalProviderCode == currentExternalGameProvider).ToList();
		}

		private void buttongetGames_Click(object sender, EventArgs e)
		{
			gridGames.DataSource = games = StuckedGames.StuckedGames.GetStuckGames(comboGameProviders.SelectedValue.ToString(), datePickerLastModifiedDate.Value);
		}

		private void buttonAnalyse_Click(object sender, EventArgs e)
		{
			if (games != null)
			{

				if (!inProcess)
				{
					inProcess = true;
					ThreadStart thStart = new ThreadStart(StartProcessing);
					workreThread = new Thread(thStart);
					workreThread.Start();
					buttonAnalyse.Text = "Stop";
					
				}
				else
				{
					workreThread.Abort();
					buttonAnalyse.Text = "Analyze";
					inProcess = false;
				}
				
			}
		}

		 

		private void StartProcessing()
		{
			List<LogEntry> logEntries = StuckGamesLogAnalyzer.GetLogs(games, currentExternalGameProvider);
			Invoke((MethodInvoker)delegate {
				stripStatus.Text = "Finished.";
				buttonAnalyse.Text = "Analyze";
				gridResult.DataSource = logEntries;
			});
			
			inProcess = false;
		}
	

		 

		private void StuckGamesLogAnalyzer_onGameProcessing(StuckGame game)
		{
			Invoke((MethodInvoker)delegate { stripStatus.Text = "Processed " + game.GameProviderSerialNumber + ". " + games.IndexOf(game) + " from " + games.Count; });
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (inProcess)
			{
				MessageBox.Show("Please stop processing");
				e.Cancel = true;
			}
		}
	}
}

