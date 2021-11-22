using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cryptography;
using Cryptography.Algorithms.Crypt;
using System.IO;
using System;
using Cryptography.Math;

namespace CryptographyUnitTest
{
    [TestClass]
    public class ECCrypt
    {
        [TestMethod]
        public void Crypt()
        {
            (int nb, ECPoint G, ECPoint Pb, EllipticCurve ecurve) = EllipticCurveCrypt.GenerateKey();
            Console.WriteLine("Открытый ключ: G=({0}, {1}), Pb=({2}, {3})", G.x, G.y, Pb.x, Pb.y);
            Console.WriteLine("Закрытый ключ: nb={0}", nb);

            byte[] text = { (byte)5 };

            var cryptedText = EllipticCurveCrypt.Encrypt(text, G, Pb, ecurve, null);
            for(int i = 0; i < cryptedText.Length; i++)
            {
                Console.Write(cryptedText[i]+" ");
            }
            Console.WriteLine();

            var decryptedText = EllipticCurveCrypt.Decrypt(cryptedText, nb, ecurve, null);
            for (int i = 0; i < decryptedText.Length; i++)
            {
                Console.Write(decryptedText[i] + " ");
            }
        }
    }
}
