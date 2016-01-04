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

namespace HexComparer
{
  partial class RegSizePrompt
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
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.lblFileName = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.lblFileSize = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.nudRegSize = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.nudRegSize)).BeginInit();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(15, 130);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(87, 23);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(113, 130);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(87, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "File:";
      // 
      // lblFileName
      // 
      this.lblFileName.AutoSize = true;
      this.lblFileName.Location = new System.Drawing.Point(56, 19);
      this.lblFileName.Name = "lblFileName";
      this.lblFileName.Size = new System.Drawing.Size(58, 13);
      this.lblFileName.TabIndex = 3;
      this.lblFileName.Text = "Filename";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(12, 46);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(38, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Size:";
      // 
      // lblFileSize
      // 
      this.lblFileSize.AutoSize = true;
      this.lblFileSize.Location = new System.Drawing.Point(56, 46);
      this.lblFileSize.Name = "lblFileSize";
      this.lblFileSize.Size = new System.Drawing.Size(48, 13);
      this.lblFileSize.TabIndex = 5;
      this.lblFileSize.Text = "Filesize";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(12, 77);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(62, 13);
      this.label5.TabIndex = 6;
      this.label5.Text = "RegSize:";
      // 
      // nudRegSize
      // 
      this.nudRegSize.Location = new System.Drawing.Point(80, 75);
      this.nudRegSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.nudRegSize.Name = "nudRegSize";
      this.nudRegSize.Size = new System.Drawing.Size(75, 21);
      this.nudRegSize.TabIndex = 7;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(161, 77);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(38, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "bytes";
      // 
      // RegSizePrompt
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(217, 170);
      this.ControlBox = false;
      this.Controls.Add(this.label2);
      this.Controls.Add(this.nudRegSize);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.lblFileSize);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.lblFileName);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "RegSizePrompt";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "RegSizePrompt";
      this.Load += new System.EventHandler(this.RegSizePrompt_Load);
      ((System.ComponentModel.ISupportInitialize)(this.nudRegSize)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblFileName;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label lblFileSize;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.NumericUpDown nudRegSize;
    private System.Windows.Forms.Label label2;
  }
}