namespace ListDebuggerVisualizer
{
  partial class MainForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.toolStripMain = new System.Windows.Forms.ToolStrip();
      this.toolStripButtonClearTypeSettings = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButtonExportToExcel = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabelTypeName = new System.Windows.Forms.ToolStripLabel();
      this.panelContent = new System.Windows.Forms.Panel();
      this.toolStripMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripMain
      // 
      this.toolStripMain.ImageScalingSize = new System.Drawing.Size(32, 32);
      this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonClearTypeSettings,
            this.toolStripSeparator1,
            this.toolStripButtonExportToExcel,
            this.toolStripSeparator2,
            this.toolStripLabelTypeName});
      this.toolStripMain.Location = new System.Drawing.Point(0, 0);
      this.toolStripMain.Name = "toolStripMain";
      this.toolStripMain.Size = new System.Drawing.Size(984, 54);
      this.toolStripMain.TabIndex = 0;
      this.toolStripMain.Text = "toolStrip1";
      // 
      // toolStripButtonClearTypeSettings
      // 
      this.toolStripButtonClearTypeSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClearTypeSettings.Image")));
      this.toolStripButtonClearTypeSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonClearTypeSettings.Name = "toolStripButtonClearTypeSettings";
      this.toolStripButtonClearTypeSettings.Size = new System.Drawing.Size(63, 51);
      this.toolStripButtonClearTypeSettings.Text = "Reset grid";
      this.toolStripButtonClearTypeSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.toolStripButtonClearTypeSettings.ToolTipText = "Reset grid";
      this.toolStripButtonClearTypeSettings.Click += new System.EventHandler(this.toolStripButtonClearTypeSettings_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
      // 
      // toolStripButtonExportToExcel
      // 
      this.toolStripButtonExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExportToExcel.Image")));
      this.toolStripButtonExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonExportToExcel.Name = "toolStripButtonExportToExcel";
      this.toolStripButtonExportToExcel.Size = new System.Drawing.Size(90, 51);
      this.toolStripButtonExportToExcel.Text = "Export To Excel";
      this.toolStripButtonExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.toolStripButtonExportToExcel.ToolTipText = "Export To Excel";
      this.toolStripButtonExportToExcel.Click += new System.EventHandler(this.toolStripButtonExportToExcel_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
      // 
      // toolStripLabelTypeName
      // 
      this.toolStripLabelTypeName.Name = "toolStripLabelTypeName";
      this.toolStripLabelTypeName.Size = new System.Drawing.Size(79, 51);
      this.toolStripLabelTypeName.Text = "Current type: ";
      // 
      // panelContent
      // 
      this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelContent.Location = new System.Drawing.Point(0, 54);
      this.panelContent.Name = "panelContent";
      this.panelContent.Size = new System.Drawing.Size(984, 556);
      this.panelContent.TabIndex = 1;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(984, 610);
      this.Controls.Add(this.panelContent);
      this.Controls.Add(this.toolStripMain);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "List Debugger Visualizer";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.toolStripMain.ResumeLayout(false);
      this.toolStripMain.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStripMain;
    private System.Windows.Forms.ToolStripButton toolStripButtonClearTypeSettings;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton toolStripButtonExportToExcel;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripLabel toolStripLabelTypeName;
    private System.Windows.Forms.Panel panelContent;

  }
}