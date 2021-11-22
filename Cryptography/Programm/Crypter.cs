using System;
using System.Collections.Generic;
using System.Text;
using Cryptography.Algorithms.Crypt;
using Cryptography.Math;

namespace Cryptography.Programm
{
    public class Crypter
    {
        public static Dictionary<byte, int> GetDict(byte[] text)
        {
            int i = 0;
            var dict = new Dictionary<byte, int>();
            foreach (byte t in text)
            {
                if (!dict.ContainsKey(t))
                {
                    dict.Add(t, i);
                    i++;
                }
            }
            return dict;
        }

        public byte[] Encrypt(byte[] text, string cipherName)
        {
            byte[] cryptedText;
            switch (cipherName)
            {
                case "FEISTELMODIFIED":
                    Console.WriteLine("\nАлогритм по сети Фейстеля: ");
                    cryptedText = FeistelModified.Encrypt(text);
                    break;
                case "MAGMA":
                    Console.WriteLine("\nШифрование Магма: ");
                    cryptedText = Magma.Encrypt(text, true);
                    break;
                case "ELGAMAL":
                    Console.WriteLine("\nШифрование Эль-Гамаля: ");
                    int p, g, y, x;
                    (p, g, y, x) = ElGamal.GenerateKey();
                    Console.WriteLine("Открытый ключ: p={0}, g={1}, y={2}", p, g, y);
                    Console.WriteLine("Закрытый ключ: x={0}", x);
                    cryptedText = ElGamal.Encrypt(text, p, g, y);
                    break;
                case "ELLIPTICCURVE":
                    (int nb, ECPoint G, ECPoint Pb, EllipticCurve ecurve) = EllipticCurveCrypt.GenerateKey();
                    Console.WriteLine("Открытый ключ: G=({0}, {1}), Pb=({2}, {3})", G.x, G.y, Pb.x, Pb.y);
                    Console.WriteLine("Закрытый ключ: nb={0}", nb);

                    // Словарь для соответствия символу алфавита числу
                    var dict = GetDict(text);
                    cryptedText = EllipticCurveCrypt.Encrypt(text, G, Pb, ecurve, dict);
                    break;
                default:
                    throw new Exception("Нет такого алгоритма");
            }

            return cryptedText;
        }
        public byte[] Decrypt(byte[] cryptedtext, string cipherName, byte[] text)
        {
            byte[] cryptedText;
            switch (cipherName)
            {
                case "FEISTELMODIFIED":
                    cryptedText = FeistelModified.Decrypt(cryptedtext);
                    FeistelModified.ShowKey();
                    break;
                case "MAGMA":
                    cryptedText = Magma.Encrypt(cryptedtext, false);
                    break;
                case "ELGAMAL":
                    int p, g, y, x;
                    (p, g, y, x) = ElGamal.GenerateKey();
                    cryptedText = ElGamal.Decrypt(cryptedtext, x,p);
                    break;
                case "ELLIPTICCURVE":
                    (int nb, ECPoint G, ECPoint Pb, EllipticCurve ecurve) = EllipticCurveCrypt.GenerateKey();
                    var dict = GetDict(text);
                    cryptedText = EllipticCurveCrypt.Decrypt(cryptedtext, nb, ecurve, dict);
                    break;
                default:
                    throw new Exception("Нет такого алгоритма");
            }

            return cryptedText;
        }
    }
}
