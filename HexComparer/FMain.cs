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
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.IO;

namespace HexComparer
{
  public partial class FMain : Form
  {
    public FMain()
    {
      InitializeComponent();
    }

    private void loadToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var ofd = new OpenFileDialog {CheckFileExists = true, Filter = "*.*|*.*", FilterIndex = 0};

      if (ofd.ShowDialog() != DialogResult.OK) return;

      var r = new RegSizePrompt {Filename = ofd.FileName};
      if (r.ShowDialog() == DialogResult.OK)
      {
        OpenFile(r.Filename, r.RegSize);
      }
    }

    List<byte[]> _allregs = new List<byte[]>();
    private void OpenFile(string filename, int regsize)
    {
      using (FileStream fs = File.Open(filename, FileMode.Open))
      {
        _allregs.Clear();
        byte[] tmp = new byte[regsize];
        int readed = fs.Read(tmp, 0, tmp.Length);
        while (readed > 0)
        {
          _allregs.Add(tmp);

          tmp = new byte[regsize];
          readed = fs.Read(tmp, 0, tmp.Length);
        }
        fs.Close();
      }

      _idx1 = 0;
      _idx2 = 1;

      if (_idx2 >= _allregs.Count)
        _idx2 = _allregs.Count - 1;

      lblIdx1.Text = _idx1.ToString(CultureInfo.InvariantCulture);
      lblIdx2.Text = _idx2.ToString(CultureInfo.InvariantCulture);
      
      ucHexViewer1.SetData(_allregs[_idx1], _allregs[_idx2]);
    }

    int _idx1;
    int _idx2;

    private void btnAnt1_Click(object sender, EventArgs e)
    {
      int prex1 = _idx1;
      int prex2 = _idx2;

      if (sender == btnAnt1)
      {
        if (_idx1 > 0)
          _idx1--;
      }
      else if (sender == btnAnt2)
      {
        if (_idx2 > 0)
          _idx2--;
      }
      else if (sender == btnPost1)
      {
        if (_idx1 < _allregs.Count - 1)
          _idx1++;
      }
      else if (sender == btnPost2)
      {
        if (_idx2 < _allregs.Count - 1)
          _idx2++;
      }

      lblIdx1.Text = _idx1.ToString(CultureInfo.InvariantCulture);
      lblIdx2.Text = _idx2.ToString(CultureInfo.InvariantCulture);

      if ((_idx1 != prex1) || (_idx2 != prex2))
      {
        var offset = ucHexViewer1.CurrentOffset;
        ucHexViewer1.SetData(_allregs[_idx1], _allregs[_idx2]);
        ucHexViewer1.CurrentOffset = offset;
      }
    }

    private void ucHexViewer1_SelectedItemChanged(object sender, HexViewerItemChangedEventArgs e)
    {
      if (ucHexViewer1.Data1 != null)
        ucViewData1.ShowValues(ucHexViewer1.Data1, e.Index);

      if (ucHexViewer1.Data2 != null)
        ucViewData2.ShowValues(ucHexViewer1.Data2, e.Index);
    }


    private void btnDecimal_Click(object sender, EventArgs e)
    {
      ucViewData1.ShowInHexadecimal = ucViewData2.ShowInHexadecimal = false;
      btnDecimal.Checked = true;
      btnHexadecimal.Checked = false;
    }

    private void btnHexadecimal_Click(object sender, EventArgs e)
    {
      ucViewData1.ShowInHexadecimal = ucViewData2.ShowInHexadecimal = true;
      btnDecimal.Checked = false;
      btnHexadecimal.Checked = true;
    }

    private void FMain_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Down:
          ucHexViewer1.SelectedIndex++;
          break;
        case Keys.Up:
          ucHexViewer1.SelectedIndex--;
          break;
      }
      e.Handled = true;
    }
  }
}
