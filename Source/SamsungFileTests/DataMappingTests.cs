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
            string[] samples = new string[] { "test 1", "čšýáířřží", "پاکستان", "is our country"};

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
