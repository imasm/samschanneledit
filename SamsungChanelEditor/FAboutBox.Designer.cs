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

namespace SamsChannelEditor
{
  sealed partial class FAboutBox
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
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
      this.labelProductName = new System.Windows.Forms.Label();
      this.labelVersion = new System.Windows.Forms.Label();
      this.labelCopyright = new System.Windows.Forms.Label();
      this.labelCompanyName = new System.Windows.Forms.Label();
      this.textBoxDescription = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.linkLabelWebsite = new System.Windows.Forms.LinkLabel();
      this.SuspendLayout();
      // 
      // labelProductName
      // 
      this.labelProductName.AutoSize = true;
      this.labelProductName.Location = new System.Drawing.Point(92, 10);
      this.labelProductName.Margin = new System.Windows.Forms.Padding(0);
      this.labelProductName.MaximumSize = new System.Drawing.Size(0, 18);
      this.labelProductName.Name = "labelProductName";
      this.labelProductName.Size = new System.Drawing.Size(95, 14);
      this.labelProductName.TabIndex = 24;
      this.labelProductName.Text = "Product Name";
      this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelVersion
      // 
      this.labelVersion.AutoSize = true;
      this.labelVersion.Location = new System.Drawing.Point(92, 35);
      this.labelVersion.Margin = new System.Windows.Forms.Padding(0);
      this.labelVersion.MaximumSize = new System.Drawing.Size(0, 18);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new System.Drawing.Size(54, 14);
      this.labelVersion.TabIndex = 23;
      this.labelVersion.Text = "Version";
      this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCopyright
      // 
      this.labelCopyright.AutoSize = true;
      this.labelCopyright.Location = new System.Drawing.Point(92, 60);
      this.labelCopyright.Margin = new System.Windows.Forms.Padding(0);
      this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 18);
      this.labelCopyright.Name = "labelCopyright";
      this.labelCopyright.Size = new System.Drawing.Size(68, 14);
      this.labelCopyright.TabIndex = 25;
      this.labelCopyright.Text = "Copyright";
      this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // labelCompanyName
      // 
      this.labelCompanyName.AutoSize = true;
      this.labelCompanyName.Location = new System.Drawing.Point(92, 85);
      this.labelCompanyName.Margin = new System.Windows.Forms.Padding(0);
      this.labelCompanyName.MaximumSize = new System.Drawing.Size(0, 18);
      this.labelCompanyName.Name = "labelCompanyName";
      this.labelCompanyName.Size = new System.Drawing.Size(106, 14);
      this.labelCompanyName.TabIndex = 26;
      this.labelCompanyName.Text = "Company Name";
      this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // textBoxDescription
      // 
      this.textBoxDescription.Location = new System.Drawing.Point(15, 138);
      this.textBoxDescription.Margin = new System.Windows.Forms.Padding(8, 3, 4, 3);
      this.textBoxDescription.Multiline = true;
      this.textBoxDescription.Name = "textBoxDescription";
      this.textBoxDescription.ReadOnly = true;
      this.textBoxDescription.Size = new System.Drawing.Size(393, 60);
      this.textBoxDescription.TabIndex = 27;
      this.textBoxDescription.TabStop = false;
      this.textBoxDescription.Text = "Description";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 10);
      this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
      this.label1.MaximumSize = new System.Drawing.Size(0, 18);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(54, 14);
      this.label1.TabIndex = 30;
      this.label1.Text = "Name :";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 35);
      this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
      this.label2.MaximumSize = new System.Drawing.Size(0, 18);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(62, 14);
      this.label2.TabIndex = 29;
      this.label2.Text = "Version:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 60);
      this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
      this.label3.MaximumSize = new System.Drawing.Size(0, 18);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(76, 14);
      this.label3.TabIndex = 31;
      this.label3.Text = "Copyright:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(12, 85);
      this.label4.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
      this.label4.MaximumSize = new System.Drawing.Size(0, 18);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(56, 14);
      this.label4.TabIndex = 32;
      this.label4.Text = "Author:";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(12, 110);
      this.label5.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
      this.label5.MaximumSize = new System.Drawing.Size(0, 18);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(48, 14);
      this.label5.TabIndex = 34;
      this.label5.Text = "www:";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // linkLabelWebsite
      // 
      this.linkLabelWebsite.AutoSize = true;
      this.linkLabelWebsite.Location = new System.Drawing.Point(92, 110);
      this.linkLabelWebsite.Name = "linkLabelWebsite";
      this.linkLabelWebsite.Size = new System.Drawing.Size(70, 14);
      this.linkLabelWebsite.TabIndex = 35;
      this.linkLabelWebsite.TabStop = true;
      this.linkLabelWebsite.Text = "linkLabel1";
      this.linkLabelWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelWebsite_LinkClicked);
      // 
      // FAboutBox
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(420, 210);
      this.Controls.Add(this.linkLabelWebsite);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.textBoxDescription);
      this.Controls.Add(this.labelProductName);
      this.Controls.Add(this.labelVersion);
      this.Controls.Add(this.labelCopyright);
      this.Controls.Add(this.labelCompanyName);
      this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FAboutBox";
      this.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About...";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelProductName;
    private System.Windows.Forms.Label labelVersion;
    private System.Windows.Forms.Label labelCopyright;
    private System.Windows.Forms.Label labelCompanyName;
    private System.Windows.Forms.TextBox textBoxDescription;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.LinkLabel linkLabelWebsite;

  }
}
