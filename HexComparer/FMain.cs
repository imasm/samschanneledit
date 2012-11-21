using System;
using System.Collections.Generic;
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
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.CheckFileExists = true;
      ofd.Filter = "*.*|*.*";
      ofd.FilterIndex = 0;
      if (ofd.ShowDialog() == DialogResult.OK)
      {
        RegSizePrompt r = new RegSizePrompt();
        r.Filename = ofd.FileName;
        if (r.ShowDialog() == DialogResult.OK)
        {
          OpenFile(r.Filename, r.RegSize);
        }
      }
    }

    List<byte[]> _allregs = new List<byte[]>();
    private void OpenFile(string filename, int regsize)
    {
      byte[] tmp = null;

      using (FileStream fs = File.Open(filename, FileMode.Open))
      {
        _allregs.Clear();
        tmp = new byte[regsize];
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

      lblIdx1.Text = _idx1.ToString();
      lblIdx2.Text = _idx2.ToString();
      
      this.ucHexViewer1.SetData(_allregs[_idx1], _allregs[_idx2]);
    }

    int _idx1 =0;
    int _idx2 =0;

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

      lblIdx1.Text = _idx1.ToString();
      lblIdx2.Text = _idx2.ToString();

      if ((_idx1 != prex1) || (_idx2 != prex2))
      {
        int offset = this.ucHexViewer1.CurrentOffset;
        this.ucHexViewer1.SetData(_allregs[_idx1], _allregs[_idx2]);
        this.ucHexViewer1.CurrentOffset = offset;
      }
    }

    private void ucHexViewer1_SelectedItemChanged(object sender, HexViewerItemChangedEventArgs e)
    {
      if (this.ucHexViewer1.Data1 != null)
        ucViewData1.ShowValues(this.ucHexViewer1.Data1, e.Index);

      if (this.ucHexViewer1.Data2 != null)
        ucViewData2.ShowValues(this.ucHexViewer1.Data2, e.Index);
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
      if (e.KeyCode == Keys.Down)
        this.ucHexViewer1.SelectedIndex++;
      else if (e.KeyCode == Keys.Up)
        this.ucHexViewer1.SelectedIndex--;
      e.Handled = true;
    }
  }
}
