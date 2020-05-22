namespace StuckBetsAnalyzer
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonGetGames = new System.Windows.Forms.Button();
			this.comboGameProviders = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboGameSubProviders = new System.Windows.Forms.ComboBox();
			this.datePickerLastModifiedDate = new System.Windows.Forms.DateTimePicker();
			this.gridGames = new System.Windows.Forms.DataGridView();
			this.buttonAnalyse = new System.Windows.Forms.Button();
			this.gridResult = new System.Windows.Forms.DataGridView();
			this.bottomBar = new System.Windows.Forms.StatusStrip();
			this.labelProcess = new System.Windows.Forms.ToolStripStatusLabel();
			this.stripStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.buttonReport = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.gridGames)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridResult)).BeginInit();
			this.bottomBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonGetGames
			// 
			this.buttonGetGames.Location = new System.Drawing.Point(655, 32);
			this.buttonGetGames.Name = "buttonGetGames";
			this.buttonGetGames.Size = new System.Drawing.Size(118, 34);
			this.buttonGetGames.TabIndex = 0;
			this.buttonGetGames.Text = "get games";
			this.buttonGetGames.UseVisualStyleBackColor = true;
			this.buttonGetGames.Click += new System.EventHandler(this.buttongetGames_Click);
			// 
			// comboGameProviders
			// 
			this.comboGameProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboGameProviders.FormattingEnabled = true;
			this.comboGameProviders.Location = new System.Drawing.Point(15, 40);
			this.comboGameProviders.Name = "comboGameProviders";
			this.comboGameProviders.Size = new System.Drawing.Size(218, 21);
			this.comboGameProviders.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Game provider";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(247, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Game sub provider";
			// 
			// comboGameSubProviders
			// 
			this.comboGameSubProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboGameSubProviders.FormattingEnabled = true;
			this.comboGameSubProviders.Location = new System.Drawing.Point(250, 40);
			this.comboGameSubProviders.Name = "comboGameSubProviders";
			this.comboGameSubProviders.Size = new System.Drawing.Size(218, 21);
			this.comboGameSubProviders.TabIndex = 4;
			// 
			// datePickerLastModifiedDate
			// 
			this.datePickerLastModifiedDate.Location = new System.Drawing.Point(496, 40);
			this.datePickerLastModifiedDate.Name = "datePickerLastModifiedDate";
			this.datePickerLastModifiedDate.Size = new System.Drawing.Size(121, 20);
			this.datePickerLastModifiedDate.TabIndex = 5;
			// 
			// gridGames
			// 
			this.gridGames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridGames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridGames.Location = new System.Drawing.Point(15, 88);
			this.gridGames.Name = "gridGames";
			this.gridGames.Size = new System.Drawing.Size(758, 208);
			this.gridGames.TabIndex = 6;
			// 
			// buttonAnalyse
			// 
			this.buttonAnalyse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAnalyse.Location = new System.Drawing.Point(655, 313);
			this.buttonAnalyse.Name = "buttonAnalyse";
			this.buttonAnalyse.Size = new System.Drawing.Size(118, 34);
			this.buttonAnalyse.TabIndex = 7;
			this.buttonAnalyse.Text = "Get logs";
			this.buttonAnalyse.UseVisualStyleBackColor = true;
			this.buttonAnalyse.Click += new System.EventHandler(this.buttonAnalyse_Click);
			// 
			// gridResult
			// 
			this.gridResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridResult.Location = new System.Drawing.Point(15, 353);
			this.gridResult.Name = "gridResult";
			this.gridResult.Size = new System.Drawing.Size(758, 337);
			this.gridResult.TabIndex = 8;
			// 
			// bottomBar
			// 
			this.bottomBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelProcess,
            this.stripStatus});
			this.bottomBar.Location = new System.Drawing.Point(0, 699);
			this.bottomBar.Name = "bottomBar";
			this.bottomBar.Size = new System.Drawing.Size(807, 22);
			this.bottomBar.TabIndex = 9;
			// 
			// labelProcess
			// 
			this.labelProcess.Name = "labelProcess";
			this.labelProcess.Size = new System.Drawing.Size(0, 17);
			// 
			// stripStatus
			// 
			this.stripStatus.Name = "stripStatus";
			this.stripStatus.Size = new System.Drawing.Size(0, 17);
			// 
			// buttonReport
			// 
			this.buttonReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReport.Location = new System.Drawing.Point(513, 313);
			this.buttonReport.Name = "buttonReport";
			this.buttonReport.Size = new System.Drawing.Size(118, 34);
			this.buttonReport.TabIndex = 10;
			this.buttonReport.Text = "Create report";
			this.buttonReport.UseVisualStyleBackColor = true;
			this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(807, 721);
			this.Controls.Add(this.buttonReport);
			this.Controls.Add(this.bottomBar);
			this.Controls.Add(this.gridResult);
			this.Controls.Add(this.buttonAnalyse);
			this.Controls.Add(this.gridGames);
			this.Controls.Add(this.datePickerLastModifiedDate);
			this.Controls.Add(this.comboGameSubProviders);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboGameProviders);
			this.Controls.Add(this.buttonGetGames);
			this.Name = "Form1";
			this.Text = "Stuck bets analyzer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridGames)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridResult)).EndInit();
			this.bottomBar.ResumeLayout(false);
			this.bottomBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonGetGames;
		private System.Windows.Forms.ComboBox comboGameProviders;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboGameSubProviders;
		private System.Windows.Forms.DateTimePicker datePickerLastModifiedDate;
		private System.Windows.Forms.DataGridView gridGames;
		private System.Windows.Forms.Button buttonAnalyse;
		private System.Windows.Forms.DataGridView gridResult;
		private System.Windows.Forms.StatusStrip bottomBar;
		private System.Windows.Forms.ToolStripStatusLabel labelProcess;
		private System.Windows.Forms.ToolStripStatusLabel stripStatus;
		private System.Windows.Forms.Button buttonReport;
	}
}

