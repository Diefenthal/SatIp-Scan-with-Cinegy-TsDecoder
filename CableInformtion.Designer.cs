namespace SatIp
{
    partial class CableInformtion
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNit = new System.Windows.Forms.Label();
            this.lblSdt = new System.Windows.Forms.Label();
            this.lblPmt = new System.Windows.Forms.Label();
            this.lblPat = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pgbSignalLevel = new System.Windows.Forms.ProgressBar();
            this.pgbSignalQuality = new System.Windows.Forms.ProgressBar();
            this.pgbSearchResult = new System.Windows.Forms.ProgressBar();
            this.lwResults = new System.Windows.Forms.ListView();
            this.colServiceType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colServiceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colServiceProvider = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colServiceId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPCR = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVideo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAudio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAC3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTTX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colsub = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxSourceA = new System.Windows.Forms.ComboBox();
            this.lblSourceA = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lblNit);
            this.groupBox3.Controls.Add(this.lblSdt);
            this.groupBox3.Controls.Add(this.lblPmt);
            this.groupBox3.Controls.Add(this.lblPat);
            this.groupBox3.Controls.Add(this.btnScan);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.pgbSignalLevel);
            this.groupBox3.Controls.Add(this.pgbSignalQuality);
            this.groupBox3.Controls.Add(this.pgbSearchResult);
            this.groupBox3.Controls.Add(this.lwResults);
            this.groupBox3.Location = new System.Drawing.Point(2, 175);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(735, 292);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(251, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 11;
            // 
            // lblNit
            // 
            this.lblNit.AutoSize = true;
            this.lblNit.BackColor = System.Drawing.Color.Red;
            this.lblNit.Location = new System.Drawing.Point(220, 17);
            this.lblNit.Name = "lblNit";
            this.lblNit.Size = new System.Drawing.Size(25, 13);
            this.lblNit.TabIndex = 10;
            this.lblNit.Text = "NIT";
            // 
            // lblSdt
            // 
            this.lblSdt.AutoSize = true;
            this.lblSdt.BackColor = System.Drawing.Color.Red;
            this.lblSdt.Location = new System.Drawing.Point(185, 17);
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.Size = new System.Drawing.Size(29, 13);
            this.lblSdt.TabIndex = 9;
            this.lblSdt.Text = "SDT";
            // 
            // lblPmt
            // 
            this.lblPmt.AutoSize = true;
            this.lblPmt.BackColor = System.Drawing.Color.Red;
            this.lblPmt.Location = new System.Drawing.Point(149, 17);
            this.lblPmt.Name = "lblPmt";
            this.lblPmt.Size = new System.Drawing.Size(30, 13);
            this.lblPmt.TabIndex = 8;
            this.lblPmt.Text = "PMT";
            // 
            // lblPat
            // 
            this.lblPat.AutoSize = true;
            this.lblPat.BackColor = System.Drawing.Color.Red;
            this.lblPat.Location = new System.Drawing.Point(115, 17);
            this.lblPat.Name = "lblPat";
            this.lblPat.Size = new System.Drawing.Size(28, 13);
            this.lblPat.TabIndex = 7;
            this.lblPat.Text = "PAT";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(583, 13);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(120, 20);
            this.btnScan.TabIndex = 6;
            this.btnScan.Text = "Start Search";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.BtnScan_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Signal Quality";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Signal Level";
            // 
            // pgbSignalLevel
            // 
            this.pgbSignalLevel.Location = new System.Drawing.Point(117, 39);
            this.pgbSignalLevel.Name = "pgbSignalLevel";
            this.pgbSignalLevel.Size = new System.Drawing.Size(612, 13);
            this.pgbSignalLevel.TabIndex = 3;
            // 
            // pgbSignalQuality
            // 
            this.pgbSignalQuality.Location = new System.Drawing.Point(117, 58);
            this.pgbSignalQuality.Name = "pgbSignalQuality";
            this.pgbSignalQuality.Size = new System.Drawing.Size(612, 13);
            this.pgbSignalQuality.TabIndex = 2;
            // 
            // pgbSearchResult
            // 
            this.pgbSearchResult.Location = new System.Drawing.Point(6, 77);
            this.pgbSearchResult.Name = "pgbSearchResult";
            this.pgbSearchResult.Size = new System.Drawing.Size(723, 10);
            this.pgbSearchResult.TabIndex = 1;
            // 
            // lwResults
            // 
            this.lwResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colServiceType,
            this.colServiceName,
            this.colServiceProvider,
            this.colServiceId,
            this.colPCR,
            this.colVideo,
            this.colAudio,
            this.colAC3,
            this.colTTX,
            this.colsub});
            this.lwResults.Location = new System.Drawing.Point(6, 93);
            this.lwResults.Name = "lwResults";
            this.lwResults.Size = new System.Drawing.Size(723, 193);
            this.lwResults.TabIndex = 0;
            this.lwResults.UseCompatibleStateImageBehavior = false;
            this.lwResults.View = System.Windows.Forms.View.Details;
            // 
            // colServiceType
            // 
            this.colServiceType.Text = "ServiceType";
            this.colServiceType.Width = 100;
            // 
            // colServiceName
            // 
            this.colServiceName.Text = "ServiceName";
            this.colServiceName.Width = 100;
            // 
            // colServiceProvider
            // 
            this.colServiceProvider.Text = "ServiceProvider";
            this.colServiceProvider.Width = 100;
            // 
            // colServiceId
            // 
            this.colServiceId.Text = "ServiceId";
            // 
            // colPCR
            // 
            this.colPCR.Text = "PCR";
            // 
            // colVideo
            // 
            this.colVideo.Text = "Video";
            // 
            // colAudio
            // 
            this.colAudio.Text = "Audio";
            // 
            // colAC3
            // 
            this.colAC3.Text = "AC3";
            // 
            // colTTX
            // 
            this.colTTX.Text = "TTX";
            // 
            // colsub
            // 
            this.colsub.Text = "Sub";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.cbxSourceA);
            this.groupBox2.Controls.Add(this.lblSourceA);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(734, 166);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sources";
            // 
            // cbxSourceA
            // 
            this.cbxSourceA.FormattingEnabled = true;
            this.cbxSourceA.Location = new System.Drawing.Point(117, 48);
            this.cbxSourceA.Name = "cbxSourceA";
            this.cbxSourceA.Size = new System.Drawing.Size(536, 21);
            this.cbxSourceA.TabIndex = 6;
            // 
            // lblSourceA
            // 
            this.lblSourceA.AutoSize = true;
            this.lblSourceA.Location = new System.Drawing.Point(7, 51);
            this.lblSourceA.Name = "lblSourceA";
            this.lblSourceA.Size = new System.Drawing.Size(51, 13);
            this.lblSourceA.TabIndex = 1;
            this.lblSourceA.Text = "Source A";
            // 
            // Cable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Cable";
            this.Size = new System.Drawing.Size(740, 469);
            this.Load += new System.EventHandler(this.Cable_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNit;
        private System.Windows.Forms.Label lblSdt;
        private System.Windows.Forms.Label lblPmt;
        private System.Windows.Forms.Label lblPat;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ProgressBar pgbSignalLevel;
        private System.Windows.Forms.ProgressBar pgbSignalQuality;
        private System.Windows.Forms.ProgressBar pgbSearchResult;
        private System.Windows.Forms.ListView lwResults;
        private System.Windows.Forms.ColumnHeader colServiceType;
        private System.Windows.Forms.ColumnHeader colServiceName;
        private System.Windows.Forms.ColumnHeader colServiceProvider;
        private System.Windows.Forms.ColumnHeader colServiceId;
        private System.Windows.Forms.ColumnHeader colPCR;
        private System.Windows.Forms.ColumnHeader colVideo;
        private System.Windows.Forms.ColumnHeader colAudio;
        private System.Windows.Forms.ColumnHeader colAC3;
        private System.Windows.Forms.ColumnHeader colTTX;
        private System.Windows.Forms.ColumnHeader colsub;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxSourceA;
        private System.Windows.Forms.Label lblSourceA;
    }
}
