namespace SatIp
{
    partial class Satellite
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
            this.columnEAC3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDTS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTTX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colsub = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxSourceD = new System.Windows.Forms.ComboBox();
            this.cbxSourceC = new System.Windows.Forms.ComboBox();
            this.cbxSourceB = new System.Windows.Forms.ComboBox();
            this.cbxSourceA = new System.Windows.Forms.ComboBox();
            this.cbxDiseqC = new System.Windows.Forms.ComboBox();
            this.lblSourceD = new System.Windows.Forms.Label();
            this.lblSourceC = new System.Windows.Forms.Label();
            this.lblSourceB = new System.Windows.Forms.Label();
            this.lblSourceA = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.groupBox3.TabIndex = 4;
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
            this.btnScan.Location = new System.Drawing.Point(609, 13);
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
            this.columnEAC3,
            this.columnAAC,
            this.columnDTS,
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
            this.colServiceType.Width = 75;
            // 
            // colServiceName
            // 
            this.colServiceName.Text = "ServiceName";
            this.colServiceName.Width = 80;
            // 
            // colServiceProvider
            // 
            this.colServiceProvider.Text = "ServiceProvider";
            this.colServiceProvider.Width = 90;
            // 
            // colServiceId
            // 
            this.colServiceId.Text = "ServiceId";
            this.colServiceId.Width = 50;
            // 
            // colPCR
            // 
            this.colPCR.Text = "PCR";
            this.colPCR.Width = 50;
            // 
            // colVideo
            // 
            this.colVideo.Text = "Video";
            this.colVideo.Width = 50;
            // 
            // colAudio
            // 
            this.colAudio.Text = "Audio";
            this.colAudio.Width = 50;
            // 
            // colAC3
            // 
            this.colAC3.Text = "AC3";
            this.colAC3.Width = 50;
            // 
            // columnEAC3
            // 
            this.columnEAC3.Text = "EAC3";
            this.columnEAC3.Width = 50;
            // 
            // columnAAC
            // 
            this.columnAAC.Text = "AAC";
            this.columnAAC.Width = 50;
            // 
            // columnDTS
            // 
            this.columnDTS.Text = "DTS";
            this.columnDTS.Width = 50;
            // 
            // colTTX
            // 
            this.colTTX.Text = "TTX";
            this.colTTX.Width = 50;
            // 
            // colsub
            // 
            this.colsub.Text = "Sub";
            this.colsub.Width = 50;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.cbxSourceD);
            this.groupBox2.Controls.Add(this.cbxSourceC);
            this.groupBox2.Controls.Add(this.cbxSourceB);
            this.groupBox2.Controls.Add(this.cbxSourceA);
            this.groupBox2.Controls.Add(this.cbxDiseqC);
            this.groupBox2.Controls.Add(this.lblSourceD);
            this.groupBox2.Controls.Add(this.lblSourceC);
            this.groupBox2.Controls.Add(this.lblSourceB);
            this.groupBox2.Controls.Add(this.lblSourceA);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(734, 166);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sources";
            // 
            // cbxSourceD
            // 
            this.cbxSourceD.FormattingEnabled = true;
            this.cbxSourceD.Location = new System.Drawing.Point(117, 132);
            this.cbxSourceD.Name = "cbxSourceD";
            this.cbxSourceD.Size = new System.Drawing.Size(536, 21);
            this.cbxSourceD.TabIndex = 9;
            this.cbxSourceD.Visible = false;
            // 
            // cbxSourceC
            // 
            this.cbxSourceC.FormattingEnabled = true;
            this.cbxSourceC.Location = new System.Drawing.Point(117, 104);
            this.cbxSourceC.Name = "cbxSourceC";
            this.cbxSourceC.Size = new System.Drawing.Size(536, 21);
            this.cbxSourceC.TabIndex = 8;
            this.cbxSourceC.Visible = false;
            // 
            // cbxSourceB
            // 
            this.cbxSourceB.FormattingEnabled = true;
            this.cbxSourceB.Location = new System.Drawing.Point(117, 76);
            this.cbxSourceB.Name = "cbxSourceB";
            this.cbxSourceB.Size = new System.Drawing.Size(536, 21);
            this.cbxSourceB.TabIndex = 7;
            this.cbxSourceB.Visible = false;
            // 
            // cbxSourceA
            // 
            this.cbxSourceA.FormattingEnabled = true;
            this.cbxSourceA.Location = new System.Drawing.Point(117, 48);
            this.cbxSourceA.Name = "cbxSourceA";
            this.cbxSourceA.Size = new System.Drawing.Size(536, 21);
            this.cbxSourceA.TabIndex = 6;
            // 
            // cbxDiseqC
            // 
            this.cbxDiseqC.FormattingEnabled = true;
            this.cbxDiseqC.Location = new System.Drawing.Point(117, 20);
            this.cbxDiseqC.Name = "cbxDiseqC";
            this.cbxDiseqC.Size = new System.Drawing.Size(132, 21);
            this.cbxDiseqC.TabIndex = 5;
            this.cbxDiseqC.SelectedIndexChanged += new System.EventHandler(this.CbxDiseqC_SelectedIndexChanged);
            // 
            // lblSourceD
            // 
            this.lblSourceD.AutoSize = true;
            this.lblSourceD.Location = new System.Drawing.Point(7, 135);
            this.lblSourceD.Name = "lblSourceD";
            this.lblSourceD.Size = new System.Drawing.Size(52, 13);
            this.lblSourceD.TabIndex = 4;
            this.lblSourceD.Text = "Source D";
            this.lblSourceD.Visible = false;
            // 
            // lblSourceC
            // 
            this.lblSourceC.AutoSize = true;
            this.lblSourceC.Location = new System.Drawing.Point(7, 107);
            this.lblSourceC.Name = "lblSourceC";
            this.lblSourceC.Size = new System.Drawing.Size(51, 13);
            this.lblSourceC.TabIndex = 3;
            this.lblSourceC.Text = "Source C";
            this.lblSourceC.Visible = false;
            // 
            // lblSourceB
            // 
            this.lblSourceB.AutoSize = true;
            this.lblSourceB.Location = new System.Drawing.Point(7, 79);
            this.lblSourceB.Name = "lblSourceB";
            this.lblSourceB.Size = new System.Drawing.Size(51, 13);
            this.lblSourceB.TabIndex = 2;
            this.lblSourceB.Text = "Source B";
            this.lblSourceB.Visible = false;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Diseq C";
            // 
            // Satellite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Satellite";
            this.Size = new System.Drawing.Size(744, 473);
            this.Load += new System.EventHandler(this.Satellite_Load);
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
        private System.Windows.Forms.ComboBox cbxSourceD;
        private System.Windows.Forms.ComboBox cbxSourceC;
        private System.Windows.Forms.ComboBox cbxSourceB;
        private System.Windows.Forms.ComboBox cbxSourceA;
        private System.Windows.Forms.ComboBox cbxDiseqC;
        private System.Windows.Forms.Label lblSourceD;
        private System.Windows.Forms.Label lblSourceC;
        private System.Windows.Forms.Label lblSourceB;
        private System.Windows.Forms.Label lblSourceA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader columnAAC;
        private System.Windows.Forms.ColumnHeader columnDTS;
        private System.Windows.Forms.ColumnHeader columnEAC3;
    }
}
