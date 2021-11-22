using System;
using System.Collections.Generic;
using System.Text;
using Cryptography.Math;

namespace Cryptography.Algorithms.Signature
{
    public static class ElGamalSignature
    {
        public static (int, int, int, int) GenerateKey()
        {
            Random rand = new Random();
            int p = 191;
            int g = PrimeNumbers.GetRoot(p);
            int x = 150; // rand.Next() % 500;
            int y = PrimeNumbers.ModularPower(g, x, p);
            return (p,g,y,x);
        }

        // Простейшая хэш-функция
        private static int Hash(byte[] text)
        {
            int result = 0;
            for(int i=0; i < text.Length; i++)
            {
                result += text[i];
                result %= 100;
            }
            return result;
        }        

        // Создание подписи
        public static (int, int) CreateSignature(byte[] Message, int p, int g, int y, int x)
        {
            int m = Hash(Message);
            int k = PrimeNumbers.MutuallyPrimeNumber(p-1);
            int r = PrimeNumbers.ModularPower(g,k,p);
            int s = PrimeNumbers.Module((m - x * r)* PrimeNumbers.Reverse(k, p-1), p-1); 
            return (r, s);
        }

        // Проверка подписи
        public static bool CheckSignature(byte[] Message, int r, int s, int p, int g, int y)
        {
            bool check = true;
            check = check && (0 < r) && (r < p) && (0 < s) && (s < p - 1);
            int m = Hash(Message);
            check = PrimeNumbers.ModularPower(g, m, p) == (PrimeNumbers.ModularPower(y, r, p) * PrimeNumbers.ModularPower(r, s, p)) % p;
            return check;
        }
    }
}
