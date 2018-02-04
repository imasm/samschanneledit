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
using System.ComponentModel;
using System.Windows.Forms;

namespace HexComparer
{
    public partial class UCViewData : UserControl
    {
        bool _showInHexadecimal;
        byte[] _lastData;
        int _lastIdx;

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

            var format = ShowInHexadecimal ? "X2" : "d";

            txtIndex.Text = idx.ToString(format);

            txtByte.Text = idx < data.Length ? data[idx].ToString(format) : "";
            txtInt16.Text = idx <= data.Length - 2 ? BitConverter.ToUInt16(data, idx).ToString(format) : "";
            txtInt32.Text = idx <= data.Length - 4 ? BitConverter.ToUInt32(data, idx).ToString(format) : "";
            txtInt64.Text = idx <= data.Length - 8 ? BitConverter.ToUInt64(data, idx).ToString(format) : "";
        }
    }
}
