using System;
using System.Collections.Generic;
using System.Text;
using Cryptography.Math;

namespace Cryptography.Algorithms.Crypt
{
    public static class ElGamal
    {
        public static (int, int, int, int) GenerateKey()
        {
            Random rand = new Random();
            int p = 211; // 103;
            int g = PrimeNumbers.GetRoot(p); // 4;
            int x = 100; // 100; // rand.Next() % 500;
            int y = PrimeNumbers.ModularPower(g, x, p);
            return (p, g, y, x);
        }

        public static byte[] Encrypt(byte[] text,int p, int g, int y) 
        {
            byte[] cryptedText = new byte[text.Length*2];
            int k;
            int a, b, m ;
            var rand = new Random();
            for(int i=0, j=0; i < text.Length; i++, j+=2)
            {
                m = text[i];

                k = rand.Next() % (p - 1);
                a = PrimeNumbers.ModularPower(g, k, p);
                b = PrimeNumbers.Module(PrimeNumbers.ModularPower(y, k, p) * m, p);
                // Console.WriteLine("{0} {1}", a, b);
                cryptedText[j] = (byte)a;
                cryptedText[j + 1] = (byte)b;
            }
            return cryptedText;
        }

        public static byte[] Decrypt(byte[] text, int x, int p)
        {
            byte[] cryptedText = new byte[text.Length / 2];
            int a, b, m;
            for (int i = 0, j=0; j < cryptedText.Length; i += 2, j++)
            {
                a = text[i];
                b = text[i + 1];
                a = PrimeNumbers.ModularPower(a, x, p);
                a = PrimeNumbers.Reverse(a, p);
                m = PrimeNumbers.Module(b * a, p);          
                cryptedText[j] = (byte)m;
            }
            return cryptedText;
        }
    }
}
