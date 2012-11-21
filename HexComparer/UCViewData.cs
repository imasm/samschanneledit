using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HexComparer
{
  public partial class UCViewData : UserControl
  {
    bool _showInHexadecimal = false;
    byte[] _lastData = null;
    int _lastIdx = 0;


    [Browsable(true)]
    public override string Text
    {
      get { return groupBox1.Text; }
      set { groupBox1.Text = value; }
    }


    [Browsable(true), DefaultValue(false)]
    public bool ShowInHexadecimal
    {
      get { return _showInHexadecimal; }
      set
      {
        _showInHexadecimal = value;
        if (_lastData != null)
          ShowValues(_lastData, _lastIdx);
      }
    }


    public UCViewData()
    {
      ShowInHexadecimal = false;
      InitializeComponent();      
    }

    public void ShowValues(byte[] data, int idx)
    {
      _lastData = data;
      _lastIdx = idx;

      string format = ShowInHexadecimal ? "X2" : "d";

      txtIndex.Text = idx.ToString(format);

      if (idx < data.Length)
        txtByte.Text = data[idx].ToString(format);
      else
        txtByte.Text = "";

      if (idx <= data.Length - 2)
        txtInt16.Text = BitConverter.ToUInt16(data, idx).ToString(format);
      else
        txtInt16.Text = "";

      if (idx <= data.Length - 4)
        txtInt32.Text = BitConverter.ToUInt32(data, idx).ToString(format);
      else
        txtInt32.Text = "";

      if (idx <= data.Length - 8)
        txtInt64.Text = BitConverter.ToUInt64(data, idx).ToString(format);
      else
        txtInt64.Text = "";
    }
  }
}
