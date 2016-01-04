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

using System;
using System.IO;
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
      var fi = new FileInfo(Filename);
      lblFileName.Text = Path.GetFileName(Filename);
      lblFileSize.Text = fi.Length + " bytes";

      if (fi.Length >= 1000)
        nudRegSize.Value = (decimal)fi.Length / 1000;
      else
        nudRegSize.Value = fi.Length;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      RegSize = (int)nudRegSize.Value;
      DialogResult = DialogResult.OK;
    }    
  }
}
