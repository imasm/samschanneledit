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
    partial class FInputBox
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
          this.label1 = new System.Windows.Forms.Label();
          this.btnOk = new System.Windows.Forms.Button();
          this.btnCancel = new System.Windows.Forms.Button();
          this.txtValor = new System.Windows.Forms.TextBox();
          this.SuspendLayout();
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label1.Location = new System.Drawing.Point(13, 20);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(75, 16);
          this.label1.TabIndex = 2;
          this.label1.Text = "InputBox";
          // 
          // btnOk
          // 
          this.btnOk.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnOk.Location = new System.Drawing.Point(16, 79);
          this.btnOk.Name = "btnOk";
          this.btnOk.Size = new System.Drawing.Size(118, 26);
          this.btnOk.TabIndex = 1;
          this.btnOk.Text = "OK";
          this.btnOk.UseVisualStyleBackColor = true;
          this.btnOk.Click += new System.EventHandler(this.button1_Click);
          // 
          // btnCancel
          // 
          this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnCancel.Location = new System.Drawing.Point(154, 79);
          this.btnCancel.Name = "btnCancel";
          this.btnCancel.Size = new System.Drawing.Size(118, 26);
          this.btnCancel.TabIndex = 2;
          this.btnCancel.Text = "CANCEL";
          this.btnCancel.UseVisualStyleBackColor = true;
          this.btnCancel.Click += new System.EventHandler(this.button2_Click);
          // 
          // txtValor
          // 
          this.txtValor.Location = new System.Drawing.Point(16, 39);
          this.txtValor.Name = "txtValor";
          this.txtValor.Size = new System.Drawing.Size(256, 22);
          this.txtValor.TabIndex = 3;
          // 
          // FInputBox
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(284, 114);
          this.Controls.Add(this.txtValor);
          this.Controls.Add(this.btnCancel);
          this.Controls.Add(this.btnOk);
          this.Controls.Add(this.label1);
          this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
          this.KeyPreview = true;
          this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
          this.Name = "FInputBox";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
          this.Text = "InputBox";
          this.Load += new System.EventHandler(this.FInputBox_Load);
          this.Activated += new System.EventHandler(this.FInputBox_Activated);
          this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FInputBox_KeyDown);
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtValor;

    }
}