using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cryptography;
using Cryptography.Algorithms.Crypt;
using System.IO;
using System;
using Cryptography.Math;
using Cryptography.Algorithms.Protocols;
using System.Text;

namespace CryptographyUnitTest
{
    [TestClass]
    public class KerberosUnitTest
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
        public void Alice()
        {
            String bobMessage = "G3EbJhsmGwEbDxsCGwEbext+GzMbDBtHGzMbDBtHGzMbcRtxGz0bARszGyY=|aMK4aMK+aMK+aMKTaMKUaMKgaMKTaMOuaMKsaMKyaMOWaC9owrJow5ZoL2jCsmjCuGjCuGjDqGjCk2jCuGjCvg==";
            var strings = bobMessage.Split("|");
            var basedText = Convert.FromBase64String((strings[0]));
            var cringe = strings[1];

            byte[] decrypted = Kerberos.DecryptBytes(basedText);
            String decryptedString = Encoding.UTF8.GetString(decrypted);
            var elems = decryptedString.Split("_");
            String Tt = elems[0];
            String L = elems[1];
            String K = elems[2].Substring(1, elems[2].Length-2);
            var keys = K.Split(", ");
            int y = Int32.Parse(keys[0]);
            int g = Int32.Parse(keys[1]);
            int p = Int32.Parse(keys[2]);
            String B = elems[3];
            Console.WriteLine(p.ToString());
            String textForBob = "10_" + Tt;

            var crypted = ElGamal.Encrypt(Encoding.UTF8.GetBytes(textForBob), p, g, y);
            Console.WriteLine(textForBob);
            String basedCrypted = Convert.ToBase64String(crypted);
            Console.WriteLine(basedCrypted + "|" + cringe);

            bobMessage = "UV5MN19R";
            basedText = Convert.FromBase64String(bobMessage);
            crypted = ElGamal.Decrypt(basedText, 5, 97);
            String cr = Encoding.UTF8.GetString(crypted);
            basedCrypted = Convert.ToBase64String(crypted);
            Console.WriteLine(cr);
        }
    }
}
