#region Copyright (C) 2011-2017 Ivan Masmitjà

// Copyright (C) 2011-2017 Ivan Masmitjà
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
using System.Globalization;
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
            Byte2 = byte2;
        }
    }

    public delegate void HexViewerItemChangedEventHandler(object sender, HexViewerItemChangedEventArgs e);

    internal sealed class HexViewerControl : Control
    {
        char[] _chars1;
        char[] _chars2;

        int _offset;

        int _recordHeight;
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

                if (_offset > MaxLen)
                    _offset = MaxLen;

                Invalidate();
            }
        }

        public int RecordsPerPage { get; private set; }
        public int MaxLen { get; private set; }

        public HexViewerControl()
        {
            TabStop = true;
            DoubleBuffered = true;
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetSelectIndex(value); }
        }

        public byte[] Data1 { get; private set; }
        public byte[] Data2 { get; private set; }

        public void SetData(byte[] d1, byte[] d2)
        {
            MaxLen = (d1.Length > d2.Length ? d1.Length : d2.Length);
            Data1 = d1;
            Data2 = d2;
            _offset = 0;

            _chars1 = Encoding.ASCII.GetChars(Data1);
            _chars2 = Encoding.ASCII.GetChars(Data2);

            _recordHeight = (int)(Font.GetHeight() * 1.3);
            RecordsPerPage = Height / _recordHeight;

            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            _recordHeight = (int)(Font.GetHeight() * 1.3);
            RecordsPerPage = Height / _recordHeight;
            //base.OnResize(e);
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            using (var p = new Pen(new SolidBrush(Color.Black), 1))
            {
                var middle = Width / 2;
                var separator = Width / 6;

                e.Graphics.FillRectangle(Brushes.DarkGray, 0, 0, separator, Height);
                e.Graphics.FillRectangle(Brushes.DarkGray, middle, 0, separator, Height);

                e.Graphics.DrawLine(p, new Point(middle, 0), new Point(middle, Height));

                p.DashPattern = new float[] { 1, 1 };

                e.Graphics.DrawLine(p, new Point(separator, 0), new Point(separator, Height));
                e.Graphics.DrawLine(p, new Point(separator * 2, 0), new Point(separator * 2, Height));
                e.Graphics.DrawLine(p, new Point(middle + separator, 0), new Point(middle + separator, Height));
                e.Graphics.DrawLine(p, new Point(middle + separator * 2, 0), new Point(middle + separator * 2, Height));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var gr = e.Graphics;

            var middle = Width / 2;
            var separator = Width / 6;

            using (Brush normalbrush = new SolidBrush(ForeColor), selbrus = new SolidBrush(Color.LightSkyBlue))
            {
                using (var p = new Pen(ForeColor, 1))
                {
                    for (var i = 0; i < RecordsPerPage; i++)
                    {
                        var current = _offset + i;
                        var currstr = string.Format("{0} ({0:X2})", current);

                        var brush = normalbrush;

                        if (current == _selectedIndex)
                            gr.FillRectangle(selbrus, 0, _recordHeight * i, Width, _recordHeight);

                        int posy;
                        if ((Data1 != null) && (current < Data1.Length))
                        {
                            posy = _recordHeight * i + (int)(_recordHeight * 0.3);
                            gr.DrawString(currstr, Font, brush, 4, posy);
                            gr.DrawString(Data1[current].ToString("X2"), Font, brush, separator + 4, posy);
                            gr.DrawString(GetChar(_chars1, current).ToString(CultureInfo.InvariantCulture),
                                          Font, brush, separator * 2 + 4, posy);
                        }

                        if ((Data2 != null) && (current < Data2.Length))
                        {
                            posy = _recordHeight * i + (int)(_recordHeight * 0.3);
                            gr.DrawString(currstr, Font, brush, middle + 4, posy);
                            gr.DrawString(Data2[current].ToString("X2"), Font, brush, middle + separator + 4, posy);
                            gr.DrawString(GetChar(_chars2, current).ToString(CultureInfo.InvariantCulture),
                                          Font, brush, middle + (separator * 2) + 4, posy);
                        }

                        gr.DrawLine(p, new Point(0, _recordHeight * (i + 1)), new Point(Width, _recordHeight * (i + 1)));
                    }
                }
            }
            base.OnPaint(e);
        }

        private static char GetChar(char[] chars, int idx)
        {
            return (chars[idx] >= 32) && (chars[idx] < 254) ? chars[idx] : ' ';
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            var record = e.Y / _recordHeight;
            SetSelectIndex(_offset + record);
            base.OnMouseDown(e);
        }

        private void SetSelectIndex(int i)
        {
            if (i < 0) i = 0;
            if (i >= MaxLen) i = MaxLen - 1;

            _selectedIndex = i;

            if (SelectedItemChanged != null)
            {
                byte b1 = 0;
                byte b2 = 0;

                if ((Data1 != null) && (Data1.Length > _selectedIndex))
                    b1 = Data1[_selectedIndex];

                if ((Data2 != null) && (Data2.Length > _selectedIndex))
                    b2 = Data2[_selectedIndex];

                SelectedItemChanged(this, new HexViewerItemChangedEventArgs(_selectedIndex, b1, b2));
            }

            Invalidate();
        }
    }
}
