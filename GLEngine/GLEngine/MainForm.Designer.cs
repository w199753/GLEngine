namespace GLEngine
{
    partial class MainForm
    {
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Pulire le risorse in uso.
		/// </summary>
		/// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Codice generato da Progettazione Windows Form

		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
            this.RenderControl = new OpenGL.GlControl();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.actionSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.actionOpenCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.actionRunApp = new System.Windows.Forms.ToolStripButton();
            this.actionDebugApp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.actionRunSandbox = new System.Windows.Forms.ToolStripButton();
            this.actionStepSandbox = new System.Windows.Forms.ToolStripButton();
            this.actionPauseSandbox = new System.Windows.Forms.ToolStripButton();
            this.actionStopSandbox = new System.Windows.Forms.ToolStripButton();
            this.splitButtonBackupSettings = new System.Windows.Forms.ToolStripSplitButton();
            this.checkBackups = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAutosave = new System.Windows.Forms.ToolStripMenuItem();
            this.optionAutosaveDisabled = new System.Windows.Forms.ToolStripMenuItem();
            this.optionAutosaveTenMinutes = new System.Windows.Forms.ToolStripMenuItem();
            this.optionAutosaveThirtyMinutes = new System.Windows.Forms.ToolStripMenuItem();
            this.optionAutoSaveOneHour = new System.Windows.Forms.ToolStripMenuItem();
            this.selectFormattingMethod = new System.Windows.Forms.ToolStripSplitButton();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // RenderControl
            // 
            this.RenderControl.Animation = true;
            this.RenderControl.AnimationTimer = false;
            this.RenderControl.BackColor = System.Drawing.Color.DimGray;
            this.RenderControl.ColorBits = ((uint)(24u));
            this.RenderControl.DepthBits = ((uint)(0u));
            this.RenderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RenderControl.Location = new System.Drawing.Point(0, 24);
            this.RenderControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.RenderControl.MultisampleBits = ((uint)(0u));
            this.RenderControl.Name = "RenderControl";
            this.RenderControl.Size = new System.Drawing.Size(853, 536);
            this.RenderControl.StencilBits = ((uint)(0u));
            this.RenderControl.TabIndex = 0;
            this.RenderControl.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_ContextCreated);
            this.RenderControl.ContextDestroying += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_ContextDestroying);
            this.RenderControl.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_Render);
            this.RenderControl.ContextUpdate += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_ContextUpdate);
            this.RenderControl.Paint += new System.Windows.Forms.PaintEventHandler(this.RenderControl_ContextPaint);
            // 
            // menuStrip
            // 
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "menuStrip1";
            this.mainMenuStrip.Size = new System.Drawing.Size(853, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(916, 639);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionSaveAll,
            this.toolStripSeparator1,
            this.actionOpenCode,
            this.toolStripSeparator2,
            this.actionRunApp,
            this.actionDebugApp,
            this.toolStripSeparator3,
            this.actionRunSandbox,
            this.actionStepSandbox,
            this.actionPauseSandbox,
            this.actionStopSandbox,
            this.splitButtonBackupSettings,
            this.selectFormattingMethod});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(916, 25);
            this.mainToolStrip.TabIndex = 4;
            // 
            // actionSaveAll
            // 
            this.actionSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.actionSaveAll.Image = global::Duality.Editor.Properties.Resources.disk_multiple;
            this.actionSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionSaveAll.Name = "actionSaveAll";
            this.actionSaveAll.Size = new System.Drawing.Size(23, 22);
            this.actionSaveAll.Text = "Save All";
            //this.actionSaveAll.Click += new System.EventHandler(this.actionSaveAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // actionOpenCode
            // 
            this.actionOpenCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.actionOpenCode.Image = global::Duality.Editor.Properties.Resources.page_white_csharp;
            this.actionOpenCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionOpenCode.Name = "actionOpenCode";
            this.actionOpenCode.Size = new System.Drawing.Size(23, 22);
            this.actionOpenCode.Text = "Open Sourcecode";
            //this.actionOpenCode.Click += new System.EventHandler(this.actionOpenCode_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // actionRunApp
            // 
            this.actionRunApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.actionRunApp.Image = global::Duality.Editor.Properties.Resources.application_go;
            this.actionRunApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionRunApp.Name = "actionRunApp";
            this.actionRunApp.Size = new System.Drawing.Size(23, 22);
            this.actionRunApp.Text = "Run Game";
            //this.actionRunApp.Click += new System.EventHandler(this.actionRunApp_Click);
            // 
            // actionDebugApp
            // 
            this.actionDebugApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.actionDebugApp.Image = global::Duality.Editor.Properties.Resources.application_bug;
            this.actionDebugApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionDebugApp.Name = "actionDebugApp";
            this.actionDebugApp.Size = new System.Drawing.Size(23, 22);
            this.actionDebugApp.Text = "Debug Game";
            //this.actionDebugApp.Click += new System.EventHandler(this.actionDebugApp_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // actionRunSandbox
            // 
            this.actionRunSandbox.AutoToolTip = false;
            this.actionRunSandbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.actionRunSandbox.Image = global::Duality.Editor.Properties.Resources.control_play_blue;
            this.actionRunSandbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionRunSandbox.Name = "actionRunSandbox";
            this.actionRunSandbox.Size = new System.Drawing.Size(23, 22);
            this.actionRunSandbox.Text = "Play";
            this.actionRunSandbox.ToolTipText = "Enter Sandbox mode";
            //this.actionRunSandbox.Click += new System.EventHandler(this.actionRunSandbox_Click);
            // 
            // actionStepSandbox
            // 
            this.actionStepSandbox.AutoToolTip = false;
            this.actionStepSandbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.actionStepSandbox.Image = global::Duality.Editor.Properties.Resources.control_step_blue;
            this.actionStepSandbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionStepSandbox.Name = "actionStepSandbox";
            this.actionStepSandbox.Size = new System.Drawing.Size(23, 22);
            this.actionStepSandbox.Text = "Step Frame";
            this.actionStepSandbox.ToolTipText = "Process one Frame";
            //this.actionStepSandbox.Click += new System.EventHandler(this.actionStepSandbox_Click);
            // 
            // actionPauseSandbox
            // 
            this.actionPauseSandbox.AutoToolTip = false;
            this.actionPauseSandbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.actionPauseSandbox.Image = global::Duality.Editor.Properties.Resources.control_pause_blue;
            this.actionPauseSandbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionPauseSandbox.Name = "actionPauseSandbox";
            this.actionPauseSandbox.Size = new System.Drawing.Size(23, 22);
            this.actionPauseSandbox.Text = "Pause";
            this.actionPauseSandbox.ToolTipText = "Pause the Sandbox";
            //this.actionPauseSandbox.Click += new System.EventHandler(this.actionPauseSandbox_Click);
            // 
            // actionStopSandbox
            // 
            this.actionStopSandbox.AutoToolTip = false;
            this.actionStopSandbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.actionStopSandbox.Image = global::Duality.Editor.Properties.Resources.control_stop_blue;
            this.actionStopSandbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionStopSandbox.Name = "actionStopSandbox";
            this.actionStopSandbox.Size = new System.Drawing.Size(23, 22);
            this.actionStopSandbox.Text = "Stop";
            this.actionStopSandbox.ToolTipText = "Leave Sandbox mode";
            //this.actionStopSandbox.Click += new System.EventHandler(this.actionStopSandbox_Click);
            // 
            // splitButtonBackupSettings
            // 
            this.splitButtonBackupSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.splitButtonBackupSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.splitButtonBackupSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkBackups,
            this.menuAutosave});
            //this.splitButtonBackupSettings.Image = global::Duality.Editor.Properties.Resources.drive_disk;
            this.splitButtonBackupSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.splitButtonBackupSettings.Name = "splitButtonBackupSettings";
            this.splitButtonBackupSettings.Size = new System.Drawing.Size(32, 22);
            this.splitButtonBackupSettings.Text = "Backup Settings";
            //this.splitButtonBackupSettings.DropDownOpening += new System.EventHandler(this.splitButtonBackupSettings_DropDownOpening);
            //this.splitButtonBackupSettings.Click += new System.EventHandler(this.splitButtonBackupSettings_Click);
            // 
            // checkBackups
            // 
            this.checkBackups.Checked = true;
            this.checkBackups.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBackups.Name = "checkBackups";
            this.checkBackups.Size = new System.Drawing.Size(123, 22);
            this.checkBackups.Text = "Backups";
            //this.checkBackups.Click += new System.EventHandler(this.checkBackups_Clicked);
            // 
            // menuAutosave
            // 
            this.menuAutosave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionAutosaveDisabled,
            this.optionAutosaveTenMinutes,
            this.optionAutosaveThirtyMinutes,
            this.optionAutoSaveOneHour});
            this.menuAutosave.Name = "menuAutosave";
            this.menuAutosave.Size = new System.Drawing.Size(123, 22);
            this.menuAutosave.Text = "Autosave";
            // 
            // optionAutosaveDisabled
            // 
            this.optionAutosaveDisabled.Name = "optionAutosaveDisabled";
            this.optionAutosaveDisabled.Size = new System.Drawing.Size(132, 22);
            this.optionAutosaveDisabled.Text = "Disabled";
            //this.optionAutosaveDisabled.Click += new System.EventHandler(this.optionAutosaveDisabled_Clicked);
            // 
            // optionAutosaveTenMinutes
            // 
            this.optionAutosaveTenMinutes.Name = "optionAutosaveTenMinutes";
            this.optionAutosaveTenMinutes.Size = new System.Drawing.Size(132, 22);
            this.optionAutosaveTenMinutes.Text = "10 Minutes";
            //this.optionAutosaveTenMinutes.Click += new System.EventHandler(this.optionAutosaveTenMinutes_Clicked);
            // 
            // optionAutosaveThirtyMinutes
            // 
            this.optionAutosaveThirtyMinutes.Checked = true;
            this.optionAutosaveThirtyMinutes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.optionAutosaveThirtyMinutes.Name = "optionAutosaveThirtyMinutes";
            this.optionAutosaveThirtyMinutes.Size = new System.Drawing.Size(132, 22);
            this.optionAutosaveThirtyMinutes.Text = "30 Minutes";
            //this.optionAutosaveThirtyMinutes.Click += new System.EventHandler(this.optionAutosaveThirtyMinutes_Clicked);
            // 
            // optionAutoSaveOneHour
            // 
            this.optionAutoSaveOneHour.Name = "optionAutoSaveOneHour";
            this.optionAutoSaveOneHour.Size = new System.Drawing.Size(132, 22);
            this.optionAutoSaveOneHour.Text = "1 Hour";
            //this.optionAutoSaveOneHour.Click += new System.EventHandler(this.optionAutoSaveOneHour_Clicked);
            // 
            // selectFormattingMethod
            // 
            this.selectFormattingMethod.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.selectFormattingMethod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.selectFormattingMethod.Image = ((System.Drawing.Image)(resources.GetObject("selectFormattingMethod.Image")));
            this.selectFormattingMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectFormattingMethod.Name = "selectFormattingMethod";
            this.selectFormattingMethod.Size = new System.Drawing.Size(32, 22);
            this.selectFormattingMethod.Text = "Project Data Format";
            //this.selectFormattingMethod.Click += new System.EventHandler(this.selectFormattingMethod_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 560);
            this.Controls.Add(this.RenderControl);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "Hello triangle";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private OpenGL.GlControl RenderControl;

		private SpTextEditorComponent spControl;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton actionRunApp;
        private System.Windows.Forms.ToolStripButton actionDebugApp;
        private System.Windows.Forms.ToolStripButton actionSaveAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton actionOpenCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton actionPauseSandbox;
        private System.Windows.Forms.ToolStripButton actionStopSandbox;
        private System.Windows.Forms.ToolStripSplitButton selectFormattingMethod;
        private System.Windows.Forms.ToolStripSplitButton splitButtonBackupSettings;
        private System.Windows.Forms.ToolStripMenuItem checkBackups;
        private System.Windows.Forms.ToolStripMenuItem menuAutosave;
        private System.Windows.Forms.ToolStripMenuItem optionAutosaveDisabled;
        private System.Windows.Forms.ToolStripMenuItem optionAutosaveTenMinutes;
        private System.Windows.Forms.ToolStripMenuItem optionAutosaveThirtyMinutes;
        private System.Windows.Forms.ToolStripMenuItem optionAutoSaveOneHour;
        private System.Windows.Forms.ToolStripButton actionRunSandbox;
        private System.Windows.Forms.ToolStripButton actionStepSandbox;
    }
}
