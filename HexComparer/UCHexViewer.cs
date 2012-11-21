using System;
using System.Windows.Forms;

namespace HexComparer
{
  public partial class UCHexViewer : UserControl
  {
    HexViewerControl hexctrl;

    public event HexViewerItemChangedEventHandler SelectedItemChanged;

    public byte[] Data1 { get { return hexctrl.Data1; } }
    public byte[] Data2 { get { return hexctrl.Data2; } }

    public int CurrentOffset
    {
      get { return hexctrl.Offset; }
      set { hexctrl.Offset = value; }
    }

    public UCHexViewer()
    {      
      InitializeComponent();      

      hexctrl = new HexViewerControl();
      hexctrl.SelectedItemChanged += new HexViewerItemChangedEventHandler(hexctrl_SelectedItemChanged);
      hexctrl.Dock = DockStyle.Fill;
      this.Controls.Add(hexctrl);
      hexctrl.BringToFront();
    }

    void hexctrl_SelectedItemChanged(object sender, HexViewerItemChangedEventArgs e)
    {
      if (SelectedItemChanged != null)
        SelectedItemChanged(sender, e);
    }

    public int SelectedIndex
    {
      get { return hexctrl.SelectedIndex; }
      set { 
        hexctrl.SelectedIndex = value;
      
      if (hexctrl.SelectedIndex < hexctrl.Offset)
        vScrollBar1.Value = Math.Max(hexctrl.SelectedIndex, 0);
      else if (hexctrl.SelectedIndex >= hexctrl.Offset + hexctrl.RecordsPerPage)
        vScrollBar1.Value = Math.Max(hexctrl.SelectedIndex - (hexctrl.RecordsPerPage - 1), 0);
      }
    }

    public void SetData(byte[] d1, byte[] d2)
    {
      hexctrl.SetData(d1, d2);
      
      if (hexctrl.RecordsPerPage < hexctrl.MaxLen)
      {
        vScrollBar1.Minimum = 0;
        vScrollBar1.Maximum = hexctrl.MaxLen;// -hexctrl.RecordsPerPage;
        vScrollBar1.SmallChange = 1;
        vScrollBar1.LargeChange = hexctrl.RecordsPerPage;
        vScrollBar1.Visible = true;
      }
      else
      {
        vScrollBar1.Visible = false;
      }
    }    

    private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
    {
      hexctrl.Offset = e.NewValue;
    }
    
  }
}
