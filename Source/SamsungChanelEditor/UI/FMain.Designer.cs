#region Copyright (C) 2011 Ivan Masmitja

// Copyright (C) 2011 Ivan Masmitja
// 
// SamsChannelEditor is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SamsChannelEditor is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with SamsChannelEditor. If not, see <http://www.gnu.org/licenses/>.

#endregion

namespace SamsChannelEditor.UI
{
  partial class FMain
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fitxerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnLoad = new System.Windows.Forms.ToolStripMenuItem();
      this.mnSave = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnDebug = new System.Windows.Forms.ToolStripMenuItem();
      this.mnRefresh = new System.Windows.Forms.ToolStripMenuItem();
      this.mnConvertToTxt = new System.Windows.Forms.ToolStripMenuItem();
      this.mnAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.treeView1 = new System.Windows.Forms.TreeView();
      this.ucSingleEdit1 = new SamsChannelEditor.UCSingleEdit();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.menuStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fitxerToolStripMenuItem,
            this.mnDebug,
            this.mnAbout});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
      this.menuStrip1.Size = new System.Drawing.Size(748, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fitxerToolStripMenuItem
      // 
      this.fitxerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnLoad,
            this.mnSave,
            this.toolStripMenuItem1,
            this.mnExit});
      this.fitxerToolStripMenuItem.Name = "fitxerToolStripMenuItem";
      this.fitxerToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fitxerToolStripMenuItem.Text = "File";
      // 
      // mnLoad
      // 
      this.mnLoad.Name = "mnLoad";
      this.mnLoad.Size = new System.Drawing.Size(100, 22);
      this.mnLoad.Text = "Load";
      this.mnLoad.Click += new System.EventHandler(this.mnLoad_Click);
      // 
      // mnSave
      // 
      this.mnSave.Name = "mnSave";
      this.mnSave.Size = new System.Drawing.Size(100, 22);
      this.mnSave.Text = "Save";
      this.mnSave.Click += new System.EventHandler(this.mnSave_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(97, 6);
      // 
      // mnExit
      // 
      this.mnExit.Name = "mnExit";
      this.mnExit.Size = new System.Drawing.Size(100, 22);
      this.mnExit.Text = "Exit";
      this.mnExit.Click += new System.EventHandler(this.mnExit_Click);
      // 
      // mnDebug
      // 
      this.mnDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnRefresh,
            this.mnConvertToTxt});
      this.mnDebug.Name = "mnDebug";
      this.mnDebug.Size = new System.Drawing.Size(54, 20);
      this.mnDebug.Text = "Debug";
      // 
      // mnRefresh
      // 
      this.mnRefresh.Name = "mnRefresh";
      this.mnRefresh.Size = new System.Drawing.Size(146, 22);
      this.mnRefresh.Text = "Refresh";
      this.mnRefresh.Click += new System.EventHandler(this.mnRefresh_Click);
      // 
      // mnConvertToTxt
      // 
      this.mnConvertToTxt.Name = "mnConvertToTxt";
      this.mnConvertToTxt.Size = new System.Drawing.Size(146, 22);
      this.mnConvertToTxt.Text = "ConvertToTxt";
      this.mnConvertToTxt.Click += new System.EventHandler(this.mnConvertToTxt_Click);
      // 
      // mnAbout
      // 
      this.mnAbout.Name = "mnAbout";
      this.mnAbout.Size = new System.Drawing.Size(61, 20);
      this.mnAbout.Text = "About...";
      this.mnAbout.Click += new System.EventHandler(this.mnAbout_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
      this.statusStrip1.Location = new System.Drawing.Point(0, 506);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
      this.statusStrip1.Size = new System.Drawing.Size(748, 22);
      this.statusStrip1.TabIndex = 2;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // statusLabel
      // 
      this.statusLabel.Name = "statusLabel";
      this.statusLabel.Size = new System.Drawing.Size(118, 17);
      this.statusLabel.Text = "toolStripStatusLabel1";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 24);
      this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.treeView1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.ucSingleEdit1);
      this.splitContainer1.Size = new System.Drawing.Size(748, 482);
      this.splitContainer1.SplitterDistance = 200;
      this.splitContainer1.TabIndex = 3;
      // 
      // treeView1
      // 
      this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView1.HideSelection = false;
      this.treeView1.Location = new System.Drawing.Point(0, 0);
      this.treeView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.treeView1.Name = "treeView1";
      this.treeView1.Size = new System.Drawing.Size(200, 482);
      this.treeView1.TabIndex = 0;
      this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
      // 
      // ucSingleEdit1
      // 
      this.ucSingleEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ucSingleEdit1.Location = new System.Drawing.Point(0, 0);
      this.ucSingleEdit1.Name = "ucSingleEdit1";
      this.ucSingleEdit1.Size = new System.Drawing.Size(544, 482);
      this.ucSingleEdit1.TabIndex = 0;
      // 
      // imageList1
      // 
      this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
      this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // FMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(748, 528);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.menuStrip1);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.Name = "FMain";
      this.Text = "Samsung Channel Editor";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FMain_KeyDown);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fitxerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnLoad;
    private System.Windows.Forms.ToolStripMenuItem mnDebug;
    private System.Windows.Forms.ToolStripMenuItem mnRefresh;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem mnExit;
    private System.Windows.Forms.ToolStripMenuItem mnConvertToTxt;
    private System.Windows.Forms.ToolStripMenuItem mnAbout;
    private System.Windows.Forms.ToolStripMenuItem mnSave;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TreeView treeView1;
    private System.Windows.Forms.ImageList imageList1;
    private UCSingleEdit ucSingleEdit1;
  }
}

