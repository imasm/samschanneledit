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
using System.Text;

namespace SamsChannelEditor.Utils
{
    public class DataMapping
    {
        private Encoding DefaultEncoding { get; set; }

        private byte[] data;
        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

        public int BaseOffset { get; set; }

        public DataMapping()
        {
            DefaultEncoding = Encoding.BigEndianUnicode;
            BaseOffset = 0;
        }

        #region Byte
        public byte GetByte(int offset)
        {
            return data[BaseOffset + offset];
        }

        public void SetByte(int offset, byte value)
        {
            data[BaseOffset + offset] = value;
        }

        #endregion

        private short GetShort(int offset)
        {
            return BitConverter.ToInt16(Data, offset);
        }

        public void SetShort(int offset, short value)
        {
            Int16 sh = value;
            byte[] b = BitConverter.GetBytes(sh);

            Data[offset] = b[0];
            Data[offset + 1] = b[1];
        }

        #region Word
        public ushort GetWord(int offset)
        {
            return BitConverter.ToUInt16(this.data, BaseOffset + offset);
        }

        public void SetWord(int offset, ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            this.data[BaseOffset + offset + 0] = b[0];
            this.data[BaseOffset + offset + 1] = b[1];
        }

        #endregion

        #region String()
        public string GetString(int offset, int length)
        {
            var encoding = this.DefaultEncoding;
            return encoding.GetString(this.data, BaseOffset + offset, length).TrimEnd('\0');
        }

        public int SetString(int offset, int length, string text)
        {
            var bytes = this.DefaultEncoding.GetBytes(text);
            int len = Math.Min(bytes.Length, length);
            Array.Clear(Data, offset, len);
            Array.Copy(bytes, 0, this.data, BaseOffset + offset, len);
            return len;
        }
        #endregion
    }
}
