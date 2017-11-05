#region Copyright (C) 2011-2017 Ivan Masmitjà

// Copyright (C) 2011-2017 Ivan Masmitjà
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

namespace HexComparer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAnt1 = new System.Windows.Forms.ToolStripButton();
            this.btnPost1 = new System.Windows.Forms.ToolStripButton();
            this.lblIdx1 = new System.Windows.Forms.ToolStripLabel();
            this.btnAnt2 = new System.Windows.Forms.ToolStripButton();
            this.btnPost2 = new System.Windows.Forms.ToolStripButton();
            this.lblIdx2 = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ToolStripViewData = new System.Windows.Forms.ToolStrip();
            this.btnDecimal = new System.Windows.Forms.ToolStripButton();
            this.btnHexadecimal = new System.Windows.Forms.ToolStripButton();
            this.ucHexViewer1 = new HexComparer.UCHexViewer();
            this.ucViewData2 = new HexComparer.UCViewData();
            this.ucViewData1 = new HexComparer.UCViewData();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.ToolStripViewData.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAnt1,
            this.btnPost1,
            this.lblIdx1,
            this.btnAnt2,
            this.btnPost2,
            this.lblIdx2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(565, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Click += new System.EventHandler(this.btnAnt1_Click);
            // 
            // btnAnt1
            // 
            this.btnAnt1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAnt1.Image = global::HexComparer.Properties.Resources.arrow_180;
            this.btnAnt1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAnt1.Name = "btnAnt1";
            this.btnAnt1.Size = new System.Drawing.Size(23, 22);
            this.btnAnt1.Text = "toolStripButton1";
            this.btnAnt1.Click += new System.EventHandler(this.btnAnt1_Click);
            // 
            // btnPost1
            // 
            this.btnPost1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPost1.Image = global::HexComparer.Properties.Resources.arrow;
            this.btnPost1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPost1.Name = "btnPost1";
            this.btnPost1.Size = new System.Drawing.Size(23, 22);
            this.btnPost1.Text = "toolStripButton2";
            this.btnPost1.Click += new System.EventHandler(this.btnAnt1_Click);
            // 
            // lblIdx1
            // 
            this.lblIdx1.AutoSize = false;
            this.lblIdx1.Name = "lblIdx1";
            this.lblIdx1.Size = new System.Drawing.Size(50, 22);
            this.lblIdx1.Text = "00";
            // 
            // btnAnt2
            // 
            this.btnAnt2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAnt2.Image = global::HexComparer.Properties.Resources.arrow_180;
            this.btnAnt2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAnt2.Name = "btnAnt2";
            this.btnAnt2.Size = new System.Drawing.Size(23, 22);
            this.btnAnt2.Text = "toolStripButton3";
            this.btnAnt2.Click += new System.EventHandler(this.btnAnt1_Click);
            // 
            // btnPost2
            // 
            this.btnPost2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPost2.Image = global::HexComparer.Properties.Resources.arrow;
            this.btnPost2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPost2.Name = "btnPost2";
            this.btnPost2.Size = new System.Drawing.Size(23, 22);
            this.btnPost2.Text = "toolStripButton4";
            this.btnPost2.Click += new System.EventHandler(this.btnAnt1_Click);
            // 
            // lblIdx2
            // 
            this.lblIdx2.AutoSize = false;
            this.lblIdx2.Name = "lblIdx2";
            this.lblIdx2.Size = new System.Drawing.Size(50, 22);
            this.lblIdx2.Text = "00";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(565, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucViewData2);
            this.panel1.Controls.Add(this.ucViewData1);
            this.panel1.Controls.Add(this.ToolStripViewData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(385, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 380);
            this.panel1.TabIndex = 3;
            // 
            // ToolStripViewData
            // 
            this.ToolStripViewData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDecimal,
            this.btnHexadecimal});
            this.ToolStripViewData.Location = new System.Drawing.Point(0, 0);
            this.ToolStripViewData.Name = "ToolStripViewData";
            this.ToolStripViewData.Size = new System.Drawing.Size(180, 25);
            this.ToolStripViewData.TabIndex = 2;
            this.ToolStripViewData.Text = "toolStrip2";
            // 
            // btnDecimal
            // 
            this.btnDecimal.Checked = true;
            this.btnDecimal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnDecimal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDecimal.Image = ((System.Drawing.Image)(resources.GetObject("btnDecimal.Image")));
            this.btnDecimal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDecimal.Name = "btnDecimal";
            this.btnDecimal.Size = new System.Drawing.Size(31, 22);
            this.btnDecimal.Text = "Dec";
            this.btnDecimal.Click += new System.EventHandler(this.btnDecimal_Click);
            // 
            // btnHexadecimal
            // 
            this.btnHexadecimal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnHexadecimal.Image = ((System.Drawing.Image)(resources.GetObject("btnHexadecimal.Image")));
            this.btnHexadecimal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHexadecimal.Name = "btnHexadecimal";
            this.btnHexadecimal.Size = new System.Drawing.Size(31, 22);
            this.btnHexadecimal.Text = "Hex";
            this.btnHexadecimal.Click += new System.EventHandler(this.btnHexadecimal_Click);
            // 
            // ucHexViewer1
            // 
            this.ucHexViewer1.CurrentOffset = 0;
            this.ucHexViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucHexViewer1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucHexViewer1.Location = new System.Drawing.Point(0, 49);
            this.ucHexViewer1.Name = "ucHexViewer1";
            this.ucHexViewer1.Size = new System.Drawing.Size(385, 380);
            this.ucHexViewer1.TabIndex = 0;
            this.ucHexViewer1.SelectedItemChanged += new HexComparer.HexViewerItemChangedEventHandler(this.ucHexViewer1_SelectedItemChanged);
            // 
            // ucViewData2
            // 
            this.ucViewData2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucViewData2.Location = new System.Drawing.Point(0, 188);
            this.ucViewData2.Name = "ucViewData2";
            this.ucViewData2.Size = new System.Drawing.Size(180, 163);
            this.ucViewData2.TabIndex = 1;
            // 
            // ucViewData1
            // 
            this.ucViewData1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucViewData1.Location = new System.Drawing.Point(0, 25);
            this.ucViewData1.Name = "ucViewData1";
            this.ucViewData1.Size = new System.Drawing.Size(180, 163);
            this.ucViewData1.TabIndex = 0;
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 429);
            this.Controls.Add(this.ucHexViewer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FMain";
            this.Text = "HexComparer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FMain_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ToolStripViewData.ResumeLayout(false);
            this.ToolStripViewData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCHexViewer ucHexViewer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAnt1;
        private System.Windows.Forms.ToolStripButton btnPost1;
        private System.Windows.Forms.ToolStripLabel lblIdx1;
        private System.Windows.Forms.ToolStripButton btnAnt2;
        private System.Windows.Forms.ToolStripButton btnPost2;
        private System.Windows.Forms.ToolStripLabel lblIdx2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private UCViewData ucViewData2;
        private UCViewData ucViewData1;
        private System.Windows.Forms.ToolStrip ToolStripViewData;
        private System.Windows.Forms.ToolStripButton btnDecimal;
        private System.Windows.Forms.ToolStripButton btnHexadecimal;

    }
}

