using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cryptography;
using Cryptography.Algorithms.Crypt;
using System.IO;
using System;
using Cryptography.Math;

namespace CryptographyUnitTest
{
    [TestClass]
    public class AESUnitTest
    {
        static void Output16(byte[,] state)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    Console.Write(Convert.ToString(state[j, k],16) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Output16all(byte[,] state)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 32; k++)
                {
                    Console.Write(Convert.ToString(state[j, k], 16) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        [TestMethod]
        public void AESShiftRows()
        {
            byte[,] bytes = { 
                { 0x01, 0x02, 0x03, 0x04 },
                { 0x01, 0x02, 0x03, 0x04 },
                { 0x01, 0x02, 0x03, 0x04 },
                { 0x01, 0x02, 0x03, 0x04 }
            };
            Output16(bytes);
            byte[] expected = {0x2a, 0x19, 0x6f, 0x34};
            byte[,] result = AES.ShiftRows(bytes, true);
            Output16(bytes);
            Output16(result);
            //Assert.AreEqual(expected, result, "Не равны");
        }

        [TestMethod]
        public void AESSubBytes()
        {
            byte[,] bytes = {
                { 0x01, 0x02, 0x03, 0x04 },
                { 0x01, 0x02, 0x03, 0x04 },
                { 0x01, 0x02, 0x03, 0x04 },
                { 0x01, 0x02, 0x03, 0x04 }
            };
            Output16(bytes);
            byte[] expected = { 0x2a, 0x19, 0x6f, 0x34 };
            byte[,] result = AES.ShiftRows(bytes, false);
            Output16(bytes);
            Output16(result);
            //Assert.AreEqual(expected, result, "Не равны");
        }

        [TestMethod]
        public void AESMixColumns()
        {
            byte[,] bytes = {
                { 0xd4, 0xbf, 0x5d, 0x30 },
                { 0xbf, 0x02, 0x03, 0x04 },
                { 0x5d, 0x02, 0x03, 0x04 },
                { 0x30, 0x02, 0x03, 0x04 }
            };
            byte[] expected = { 0x2a, 0x19, 0x6f, 0x34 };
            byte[,] result = AES.MixColumns(bytes, true);
            Output16(bytes);
            Output16(result);
            //Assert.AreEqual(expected, result, "Не равны");
        }

        [TestMethod]
        public void AESKeys()
        {
            byte[,] bytes = {
                { 0xf5, 0x01, 0xae, 0x88 },
                { 0xd5, 0xd2, 0x12, 0x3b },
                { 0x9a, 0x13, 0xc3, 0xbb },
                { 0x32, 0x7a, 0xc7, 0x36 }
            };
            byte[] expected = { 0x2a, 0x19, 0x6f, 0x34 };
            var result = AES.KeyExpansion(bytes);

            Output16(bytes);
            Output16all(result);
            //Assert.AreEqual(expected, result, "Не равны");
        }
    }
}
