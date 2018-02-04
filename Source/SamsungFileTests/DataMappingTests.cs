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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamsChannelEditor.Utils;

namespace SamsungFileTests
{
    [TestClass]
    public class DataMappingTests
    {
        [TestMethod]
        public void TestByte()
        {
            DataMapping mapping = new DataMapping();
            byte[] data = new byte[12];
            for (byte i = byte.MinValue; i < byte.MaxValue; i++)
            {
                Array.Clear(data, 0, 12);
                mapping.Data = data;
                mapping.SetByte(0, i);
                Assert.AreEqual(i, mapping.GetByte(0));
            }

            Array.Clear(data, 0, 12);
            mapping.Data = data;
            mapping.SetByte(0, byte.MaxValue);
            Assert.AreEqual(byte.MaxValue, mapping.GetByte(0));
        }

        [TestMethod]
        public void TestWord()
        {
            DataMapping mapping = new DataMapping();
            byte[] data = new byte[12];
            for (ushort i = ushort.MinValue; i < ushort.MaxValue; i++)
            {
                Array.Clear(data, 0, 12);
                mapping.Data = data;
                mapping.SetWord(0, i);
                Assert.AreEqual(i, mapping.GetWord(0));
            }

            Array.Clear(data, 0, 12);
            mapping.Data = data;
            mapping.SetWord(0, ushort.MaxValue);
            Assert.AreEqual(ushort.MaxValue, mapping.GetWord(0));
        }

        [TestMethod]
        public void TestString()
        {
            string[] samples = new string[] { "test 1", "čšýáířřží", "پاکستان", "is our country" };

            DataMapping mapping = new DataMapping();
            byte[] data = new byte[200];

            foreach (var str in samples)
            {
                Array.Clear(data, 0, 200);
                mapping.Data = data;
                mapping.SetString(0, 200, str);
                Assert.AreEqual(str, mapping.GetString(0, 200));
            }
        }
    }
}
