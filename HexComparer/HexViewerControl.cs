using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HexComparer
{
  public class HexViewerItemChangedEventArgs : EventArgs
  {
    public int Index { get; private set; }
    public byte Byte1 { get; private set; }
    public byte Byte2 { get; private set; }

    public HexViewerItemChangedEventArgs(int idx, byte byte1, byte byte2)
    {
      Index = idx;
      Byte1 = byte1;
      Byte1 = byte2;
    }
  }

  public delegate void HexViewerItemChangedEventHandler(object sender, HexViewerItemChangedEventArgs e);

  internal class HexViewerControl : Control
  {
    
    byte[] _data1 = null;
    byte[] _data2 = null;
    char[] _chars1 = null;
    char[] _chars2 = null;

    int _maxlen = 0;
    int _offset = 0;

    int _recordHeight = 0;
    int _recordsPerPage = 0;
    int _selectedIndex = -1;

    public event HexViewerItemChangedEventHandler SelectedItemChanged;

    public int Offset
    {
      get { return _offset; }
      set
      {
        _offset = value;
        if (_offset < 0)
          _offset = 0;

        if (_offset > _maxlen)
          _offset = _maxlen;

        Invalidate();
      }
    }

    public int RecordsPerPage
    {
      get { return _recordsPerPage; }
    }

    public int MaxLen
    {
      get { return _maxlen; }
    }

    public HexViewerControl() :
      base()
    {
      this.TabStop = true;
      this.DoubleBuffered = true;
    }

    public int SelectedIndex
    {
      get { return _selectedIndex; }
      set { SetSelectIndex(value); }
    }

    public byte[] Data1 { get { return _data1; } }
    public byte[] Data2 { get { return _data2; } }

    public void SetData(byte[] d1, byte[] d2)
    {
      _maxlen = (d1.Length > d2.Length ? d1.Length : d2.Length);
      _data1 = d1;
      _data2 = d2;
      _offset = 0;

      _chars1 = Encoding.ASCII.GetChars(_data1);
      _chars2 = Encoding.ASCII.GetChars(_data2);

      _recordHeight = (int)(this.Font.GetHeight() * 1.3);
      _recordsPerPage = this.Height / _recordHeight;

      Invalidate();
    }

    protected override void OnResize(EventArgs e)
    {
      _recordHeight = (int)(this.Font.GetHeight() * 1.3);
      _recordsPerPage = this.Height / _recordHeight;
      //base.OnResize(e);
      Invalidate();
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      e.Graphics.Clear(this.BackColor);
      using (Pen p = new Pen(new SolidBrush(Color.Black), 1))
      {
        int middle = this.Width / 2;
        int separator = this.Width / 6;

        e.Graphics.FillRectangle(Brushes.DarkGray, 0, 0, separator, this.Height);
        e.Graphics.FillRectangle(Brushes.DarkGray, middle, 0, separator, this.Height);

        e.Graphics.DrawLine(p, new Point(middle, 0), new Point(middle, this.Height));

        p.DashPattern = new float[] { 1, 1 };

        e.Graphics.DrawLine(p, new Point(separator, 0), new Point(separator, this.Height));
        e.Graphics.DrawLine(p, new Point(separator * 2, 0), new Point(separator * 2, this.Height));
        e.Graphics.DrawLine(p, new Point(middle + separator, 0), new Point(middle + separator, this.Height));
        e.Graphics.DrawLine(p, new Point(middle + separator * 2, 0), new Point(middle + separator * 2, this.Height));
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics gr = e.Graphics;
      int posy;
      int current;

      int middle = this.Width / 2;
      int separator = this.Width / 6;
      string currstr = "";
      Brush brush;

      using (Brush normalbrush = new SolidBrush(this.ForeColor), selbrus = new SolidBrush(Color.LightSkyBlue))
      {
        using (Pen p = new Pen(this.ForeColor, 1))
        {
          for (int i = 0; i < _recordsPerPage; i++)
          {
            current = _offset + i;
            currstr = string.Format("{0} ({0:X2})", current);

            brush = normalbrush;

            if (current == _selectedIndex)
              gr.FillRectangle(selbrus, 0, _recordHeight * i, this.Width, _recordHeight);

            if ((_data1 != null) && (current < _data1.Length))
            {
              posy = _recordHeight * i + (int)(_recordHeight * 0.3);
              gr.DrawString(currstr, this.Font, brush, 4, posy);
              gr.DrawString(_data1[current].ToString("X2"), this.Font, brush, separator + 4, posy);
              gr.DrawString(GetChar(_chars1, current).ToString(), this.Font, brush, separator * 2 + 4, posy);
            }

            if ((_data2 != null) && (current < _data2.Length))
            {
              posy = _recordHeight * i + (int)(_recordHeight * 0.3);
              gr.DrawString(currstr, this.Font, brush, middle + 4, posy);
              gr.DrawString(_data2[current].ToString("X2"), this.Font, brush, middle + separator + 4, posy);
              gr.DrawString(GetChar(_chars2, current).ToString(), this.Font, brush, middle + (separator * 2) + 4, posy);
            }

            gr.DrawLine(p, new Point(0, _recordHeight * (i + 1)), new Point(this.Width, _recordHeight * (i + 1)));
          }
        }
      }

      SizeF sizeText = gr.MeasureString(Text, Font);
      base.OnPaint(e);
    }

    private char GetChar(char[] chars, int idx)
    {
      if ((chars[idx] >= 32) && (chars[idx] < 254))
        return chars[idx];
      else
        return ' ';
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      int middle = this.Width / 2;

      int size = (e.X < middle ? 1 : 2);
      int record = e.Y / _recordHeight;
      SetSelectIndex(_offset + record);      
      base.OnMouseDown(e);
    }

    private void SetSelectIndex(int i)
    {      
      if (i < 0) i = 0;
      if (i >= _maxlen) i = _maxlen - 1;

      _selectedIndex = i;

      if (SelectedItemChanged != null)
      {
        byte b1 = 0;
        byte b2 = 0;

        if ((_data1 != null) && (_data1.Length > _selectedIndex))
          b1 = _data1[_selectedIndex];

        if ((_data2 != null) && (_data2.Length > _selectedIndex))
          b2 = _data2[_selectedIndex];

        SelectedItemChanged(this, new HexViewerItemChangedEventArgs(_selectedIndex, b1, b2));
      }

      Invalidate();
    }
  }
}
