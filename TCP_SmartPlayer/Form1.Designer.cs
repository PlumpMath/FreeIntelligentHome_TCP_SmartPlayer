namespace TCP_SmartPlayer
{
    partial class Form1
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmdPlayFile = new System.Windows.Forms.Button();
            this.lstCmd = new System.Windows.Forms.ListBox();
            this.timerCmd = new System.Windows.Forms.Timer(this.components);
            this.cmdChooseDownloadDir = new System.Windows.Forms.Button();
            this.lstPlayable = new System.Windows.Forms.ListBox();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdPlayPause = new System.Windows.Forms.Button();
            this.cmdPrev = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdLauter = new System.Windows.Forms.Button();
            this.cmdLeiser = new System.Windows.Forms.Button();
            this.axVLC = new AxAXVLC.AxVLCPlugin();
            this.cmdSchrittVor = new System.Windows.Forms.Button();
            this.cmdSchrittZurueck = new System.Windows.Forms.Button();
            this.tmpLabel = new System.Windows.Forms.Label();
            this.cmdChooseOptDir = new System.Windows.Forms.Button();
            this.chkShutdownHib = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblShutdownState = new System.Windows.Forms.Label();
            this.cmdToggleShutdown = new System.Windows.Forms.Button();
            this.cmdChooseArchiveDir = new System.Windows.Forms.Button();
            this.cmdArchivieren = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axVLC)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdPlayFile
            // 
            this.cmdPlayFile.Location = new System.Drawing.Point(12, 38);
            this.cmdPlayFile.Name = "cmdPlayFile";
            this.cmdPlayFile.Size = new System.Drawing.Size(148, 23);
            this.cmdPlayFile.TabIndex = 0;
            this.cmdPlayFile.Text = "Öffne Datei";
            this.cmdPlayFile.UseVisualStyleBackColor = true;
            this.cmdPlayFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstCmd
            // 
            this.lstCmd.FormattingEnabled = true;
            this.lstCmd.HorizontalScrollbar = true;
            this.lstCmd.Location = new System.Drawing.Point(13, 606);
            this.lstCmd.Name = "lstCmd";
            this.lstCmd.Size = new System.Drawing.Size(1150, 82);
            this.lstCmd.TabIndex = 2;
            // 
            // timerCmd
            // 
            this.timerCmd.Enabled = true;
            this.timerCmd.Interval = 10;
            this.timerCmd.Tick += new System.EventHandler(this.timerCmd_Tick);
            // 
            // cmdChooseDownloadDir
            // 
            this.cmdChooseDownloadDir.Location = new System.Drawing.Point(12, 12);
            this.cmdChooseDownloadDir.Name = "cmdChooseDownloadDir";
            this.cmdChooseDownloadDir.Size = new System.Drawing.Size(148, 23);
            this.cmdChooseDownloadDir.TabIndex = 4;
            this.cmdChooseDownloadDir.Text = "Wähle Medien Ordner";
            this.cmdChooseDownloadDir.UseVisualStyleBackColor = true;
            this.cmdChooseDownloadDir.Click += new System.EventHandler(this.cmdChooseDownloadDir_Click);
            // 
            // lstPlayable
            // 
            this.lstPlayable.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlayable.FormattingEnabled = true;
            this.lstPlayable.HorizontalScrollbar = true;
            this.lstPlayable.ItemHeight = 32;
            this.lstPlayable.Location = new System.Drawing.Point(12, 300);
            this.lstPlayable.Name = "lstPlayable";
            this.lstPlayable.Size = new System.Drawing.Size(1150, 292);
            this.lstPlayable.TabIndex = 5;
            this.lstPlayable.DoubleClick += new System.EventHandler(this.lstPlayable_DoubleClick);
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(85, 64);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(75, 23);
            this.cmdNext.TabIndex = 6;
            this.cmdNext.Text = "Next";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // cmdPlayPause
            // 
            this.cmdPlayPause.Location = new System.Drawing.Point(12, 90);
            this.cmdPlayPause.Name = "cmdPlayPause";
            this.cmdPlayPause.Size = new System.Drawing.Size(148, 23);
            this.cmdPlayPause.TabIndex = 7;
            this.cmdPlayPause.Text = "Play/Pause";
            this.cmdPlayPause.UseVisualStyleBackColor = true;
            this.cmdPlayPause.Click += new System.EventHandler(this.cmdPlayPause_Click);
            // 
            // cmdPrev
            // 
            this.cmdPrev.Location = new System.Drawing.Point(12, 64);
            this.cmdPrev.Name = "cmdPrev";
            this.cmdPrev.Size = new System.Drawing.Size(67, 23);
            this.cmdPrev.TabIndex = 10;
            this.cmdPrev.Text = "Prev";
            this.cmdPrev.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 32);
            this.label2.TabIndex = 12;
            this.label2.Text = "Auswahl";
            // 
            // cmdLauter
            // 
            this.cmdLauter.Location = new System.Drawing.Point(12, 116);
            this.cmdLauter.Name = "cmdLauter";
            this.cmdLauter.Size = new System.Drawing.Size(67, 23);
            this.cmdLauter.TabIndex = 14;
            this.cmdLauter.Text = "Lauter";
            this.cmdLauter.UseVisualStyleBackColor = true;
            this.cmdLauter.Click += new System.EventHandler(this.cmdLauter_Click);
            // 
            // cmdLeiser
            // 
            this.cmdLeiser.Location = new System.Drawing.Point(85, 116);
            this.cmdLeiser.Name = "cmdLeiser";
            this.cmdLeiser.Size = new System.Drawing.Size(75, 23);
            this.cmdLeiser.TabIndex = 13;
            this.cmdLeiser.Text = "Leiser";
            this.cmdLeiser.UseVisualStyleBackColor = true;
            this.cmdLeiser.Click += new System.EventHandler(this.cmdLeiser_Click);
            // 
            // axVLC
            // 
            this.axVLC.Enabled = true;
            this.axVLC.Location = new System.Drawing.Point(881, 12);
            this.axVLC.Name = "axVLC";
            this.axVLC.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVLC.OcxState")));
            this.axVLC.Size = new System.Drawing.Size(281, 154);
            this.axVLC.TabIndex = 15;
            // 
            // cmdSchrittVor
            // 
            this.cmdSchrittVor.Location = new System.Drawing.Point(12, 143);
            this.cmdSchrittVor.Name = "cmdSchrittVor";
            this.cmdSchrittVor.Size = new System.Drawing.Size(67, 23);
            this.cmdSchrittVor.TabIndex = 17;
            this.cmdSchrittVor.Text = "Schritt Vor";
            this.cmdSchrittVor.UseVisualStyleBackColor = true;
            this.cmdSchrittVor.Click += new System.EventHandler(this.cmdSchrittVor_Click);
            // 
            // cmdSchrittZurueck
            // 
            this.cmdSchrittZurueck.Location = new System.Drawing.Point(85, 143);
            this.cmdSchrittZurueck.Name = "cmdSchrittZurueck";
            this.cmdSchrittZurueck.Size = new System.Drawing.Size(75, 23);
            this.cmdSchrittZurueck.TabIndex = 16;
            this.cmdSchrittZurueck.Text = "Zurück";
            this.cmdSchrittZurueck.UseVisualStyleBackColor = true;
            this.cmdSchrittZurueck.Click += new System.EventHandler(this.cmdSchrittZurueck_Click);
            // 
            // tmpLabel
            // 
            this.tmpLabel.AutoSize = true;
            this.tmpLabel.BackColor = System.Drawing.Color.Transparent;
            this.tmpLabel.Font = new System.Drawing.Font("Cambria", 20.25F);
            this.tmpLabel.Location = new System.Drawing.Point(537, 175);
            this.tmpLabel.Name = "tmpLabel";
            this.tmpLabel.Size = new System.Drawing.Size(85, 32);
            this.tmpLabel.TabIndex = 18;
            this.tmpLabel.Text = "label1";
            // 
            // cmdChooseOptDir
            // 
            this.cmdChooseOptDir.Location = new System.Drawing.Point(166, 12);
            this.cmdChooseOptDir.Name = "cmdChooseOptDir";
            this.cmdChooseOptDir.Size = new System.Drawing.Size(148, 23);
            this.cmdChooseOptDir.TabIndex = 19;
            this.cmdChooseOptDir.Text = "Wähle Zweiten Ordner(opt)";
            this.cmdChooseOptDir.UseVisualStyleBackColor = true;
            this.cmdChooseOptDir.Click += new System.EventHandler(this.cmdChooseOptDir_Click);
            // 
            // chkShutdownHib
            // 
            this.chkShutdownHib.AutoSize = true;
            this.chkShutdownHib.Location = new System.Drawing.Point(166, 58);
            this.chkShutdownHib.Name = "chkShutdownHib";
            this.chkShutdownHib.Size = new System.Drawing.Size(74, 17);
            this.chkShutdownHib.TabIndex = 20;
            this.chkShutdownHib.Text = "Shutdown";
            this.chkShutdownHib.UseVisualStyleBackColor = true;
            this.chkShutdownHib.CheckedChanged += new System.EventHandler(this.chkShutdownHib_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Shutdown oder Schlafmodus";
            // 
            // lblShutdownState
            // 
            this.lblShutdownState.AutoSize = true;
            this.lblShutdownState.BackColor = System.Drawing.Color.Transparent;
            this.lblShutdownState.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShutdownState.Location = new System.Drawing.Point(320, 80);
            this.lblShutdownState.Name = "lblShutdownState";
            this.lblShutdownState.Size = new System.Drawing.Size(153, 32);
            this.lblShutdownState.TabIndex = 22;
            this.lblShutdownState.Text = "Deaktiviert";
            // 
            // cmdToggleShutdown
            // 
            this.cmdToggleShutdown.Location = new System.Drawing.Point(166, 90);
            this.cmdToggleShutdown.Name = "cmdToggleShutdown";
            this.cmdToggleShutdown.Size = new System.Drawing.Size(148, 23);
            this.cmdToggleShutdown.TabIndex = 23;
            this.cmdToggleShutdown.Text = "Shutdown nach Track";
            this.cmdToggleShutdown.UseVisualStyleBackColor = true;
            this.cmdToggleShutdown.Click += new System.EventHandler(this.cmdToggleShutdown_Click);
            // 
            // cmdChooseArchiveDir
            // 
            this.cmdChooseArchiveDir.Location = new System.Drawing.Point(166, 116);
            this.cmdChooseArchiveDir.Name = "cmdChooseArchiveDir";
            this.cmdChooseArchiveDir.Size = new System.Drawing.Size(148, 23);
            this.cmdChooseArchiveDir.TabIndex = 24;
            this.cmdChooseArchiveDir.Text = "Wähle Archiv Ordner";
            this.cmdChooseArchiveDir.UseVisualStyleBackColor = true;
            this.cmdChooseArchiveDir.Click += new System.EventHandler(this.cmdChooseArchiveDir_Click);
            // 
            // cmdArchivieren
            // 
            this.cmdArchivieren.Location = new System.Drawing.Point(166, 143);
            this.cmdArchivieren.Name = "cmdArchivieren";
            this.cmdArchivieren.Size = new System.Drawing.Size(148, 23);
            this.cmdArchivieren.TabIndex = 27;
            this.cmdArchivieren.Text = "Archivieren";
            this.cmdArchivieren.UseVisualStyleBackColor = true;
            this.cmdArchivieren.Click += new System.EventHandler(this.cmdArchivieren_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 696);
            this.Controls.Add(this.cmdArchivieren);
            this.Controls.Add(this.cmdChooseArchiveDir);
            this.Controls.Add(this.cmdToggleShutdown);
            this.Controls.Add(this.lblShutdownState);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkShutdownHib);
            this.Controls.Add(this.cmdChooseOptDir);
            this.Controls.Add(this.tmpLabel);
            this.Controls.Add(this.cmdSchrittVor);
            this.Controls.Add(this.cmdSchrittZurueck);
            this.Controls.Add(this.axVLC);
            this.Controls.Add(this.cmdLauter);
            this.Controls.Add(this.cmdLeiser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdPrev);
            this.Controls.Add(this.cmdPlayPause);
            this.Controls.Add(this.cmdNext);
            this.Controls.Add(this.lstPlayable);
            this.Controls.Add(this.cmdChooseDownloadDir);
            this.Controls.Add(this.lstCmd);
            this.Controls.Add(this.cmdPlayFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "TCP_SmartPlayer v1.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.axVLC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdPlayFile;
        private System.Windows.Forms.ListBox lstCmd;
        private System.Windows.Forms.Timer timerCmd;
        private System.Windows.Forms.Button cmdChooseDownloadDir;
        private System.Windows.Forms.ListBox lstPlayable;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.Button cmdPlayPause;
        private System.Windows.Forms.Button cmdPrev;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdLauter;
        private System.Windows.Forms.Button cmdLeiser;
        private AxAXVLC.AxVLCPlugin axVLC;
        private System.Windows.Forms.Button cmdSchrittVor;
        private System.Windows.Forms.Button cmdSchrittZurueck;
        private System.Windows.Forms.Label tmpLabel;
        private System.Windows.Forms.Button cmdChooseOptDir;
        private System.Windows.Forms.CheckBox chkShutdownHib;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblShutdownState;
        private System.Windows.Forms.Button cmdToggleShutdown;
        private System.Windows.Forms.Button cmdChooseArchiveDir;
        private System.Windows.Forms.Button cmdArchivieren;
    }
}

