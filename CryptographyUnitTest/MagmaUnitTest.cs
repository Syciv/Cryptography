using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cryptography;
using Cryptography.Algorithms.Crypt;
using System.IO;
using System;

namespace CryptographyUnitTest
{
    [TestClass]
    public class MagmaunitTest
    {
        static void Output16(byte[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(Convert.ToString(text[i], 16) + " ");
            }
            Console.WriteLine();
        }

        [TestMethod]
        public void MagmaKeys()
        {
            var bytes = Magma.GenerateKeys();
        }

        [TestMethod]
        public void MagmaTransformation()
        {
            byte[] bytes = {0xfd, 0xb9, 0x75, 0x31};
            Output16(bytes);
            byte[] expected = {0x2a, 0x19, 0x6f, 0x34};
            byte[] result = Magma.Magma_T(bytes);
            Output16(bytes);
            Output16(result);
            Assert.AreEqual(expected, result, "Не равны");
        }

        [TestMethod]
        public void Magma_g()
        {
            byte[] bytes = { 0xfe, 0xdc, 0xba, 0x98 };
            byte[] key = { 0x87, 0x65, 0x43, 0x21 };
            byte[] expected = { 0xfd, 0xcb, 0xc2, 0x0c };
            byte[] result = Magma.Magma_g(bytes, key);
            Output16(bytes);
            Output16(result);
           // Assert.AreEqual(expected, result, "Не равны");
        }

        [TestMethod]
        public void MagmaSumm()
        {
            UInt32 num1 = 75432623;
            UInt32 num2 = 754623;
            UInt32 expected = 76187246;
            byte[] result = Magma.SummMod32(BitConverter.GetBytes(num1), BitConverter.GetBytes(num2));
            Output16(result);
            Output16(BitConverter.GetBytes(BitConverter.ToUInt32(result)));
            Console.WriteLine(BitConverter.ToUInt32(result));
            Assert.AreEqual(expected, BitConverter.ToUInt32(result), "Не равны");
        }

        [TestMethod]
        public void xor()
        {
            byte[] intResultLeft = Magma.IntToBytes(754623);
            UInt32 intResultRight = 434254252;
            UInt32 expected = 434745875;
            UInt32 result = Magma.BytesToInt(intResultLeft) ^ intResultRight;
            Console.WriteLine(result.ToString());
            Assert.AreEqual(expected, result, "Не равны");
        }
    }
}
