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
using System.Windows.Forms;

namespace HexComparer
{
  public partial class UCHexViewer : UserControl
  {
    readonly HexViewerControl _hexctrl;

    public event HexViewerItemChangedEventHandler SelectedItemChanged;

    public byte[] Data1 { get { return _hexctrl.Data1; } }
    public byte[] Data2 { get { return _hexctrl.Data2; } }

    public int CurrentOffset
    {
      get { return _hexctrl.Offset; }
      set { _hexctrl.Offset = value; }
    }

    public UCHexViewer()
    {      
      InitializeComponent();      

      _hexctrl = new HexViewerControl();
      _hexctrl.SelectedItemChanged += hexctrl_SelectedItemChanged;
      _hexctrl.Dock = DockStyle.Fill;
      Controls.Add(_hexctrl);
      _hexctrl.BringToFront();
    }

    void hexctrl_SelectedItemChanged(object sender, HexViewerItemChangedEventArgs e)
    {
      if (SelectedItemChanged != null)
        SelectedItemChanged(sender, e);
    }

    public int SelectedIndex
    {
      get { return _hexctrl.SelectedIndex; }
      set { 
        _hexctrl.SelectedIndex = value;
      
      if (_hexctrl.SelectedIndex < _hexctrl.Offset)
        vScrollBar1.Value = Math.Max(_hexctrl.SelectedIndex, 0);
      else if (_hexctrl.SelectedIndex >= _hexctrl.Offset + _hexctrl.RecordsPerPage)
        vScrollBar1.Value = Math.Max(_hexctrl.SelectedIndex - (_hexctrl.RecordsPerPage - 1), 0);
      }
    }

    public void SetData(byte[] d1, byte[] d2)
    {
      _hexctrl.SetData(d1, d2);
      
      if (_hexctrl.RecordsPerPage < _hexctrl.MaxLen)
      {
        vScrollBar1.Minimum = 0;
        vScrollBar1.Maximum = _hexctrl.MaxLen;// -hexctrl.RecordsPerPage;
        vScrollBar1.SmallChange = 1;
        vScrollBar1.LargeChange = _hexctrl.RecordsPerPage;
        vScrollBar1.Visible = true;
      }
      else
      {
        vScrollBar1.Visible = false;
      }
    }    

    private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
    {
      _hexctrl.Offset = e.NewValue;
    }
    
  }
}
