using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HexComparer
{
  public partial class RegSizePrompt : Form
  {
    public string Filename {get; set; }
    public int RegSize { get; private set; }

    public RegSizePrompt()
    {
      InitializeComponent();
      RegSize = 0;
    }

    private void RegSizePrompt_Load(object sender, EventArgs e)
    {
      FileInfo fi = new FileInfo(this.Filename);
      lblFileName.Text = Path.GetFileName(this.Filename);
      lblFileSize.Text = fi.Length.ToString() + " bytes";

      if (fi.Length >= 1000)
        nudRegSize.Value = fi.Length / 1000;
      else
        nudRegSize.Value = fi.Length;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      this.RegSize = (int)nudRegSize.Value;
      this.DialogResult = DialogResult.OK;
    }    
  }
}
