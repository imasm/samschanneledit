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
