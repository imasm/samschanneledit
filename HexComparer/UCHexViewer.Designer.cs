namespace HexComparer
{
  partial class UCHexViewer
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
      this.SuspendLayout();
      // 
      // vScrollBar1
      // 
      this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
      this.vScrollBar1.Location = new System.Drawing.Point(299, 0);
      this.vScrollBar1.Name = "vScrollBar1";
      this.vScrollBar1.Size = new System.Drawing.Size(17, 348);
      this.vScrollBar1.TabIndex = 0;
      this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
      // 
      // UCHexViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.vScrollBar1);
      this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "UCHexViewer";
      this.Size = new System.Drawing.Size(316, 348);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.VScrollBar vScrollBar1;
  }
}
