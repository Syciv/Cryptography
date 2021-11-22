using System;
using System.Collections.Generic;
using System.Text;
using Cryptography.Math;

namespace Cryptography.Algorithms.RandomNumbers
{
    public static class BlumBlumShub
    {
        // Настоящее случайное простое число
        public static int TrueRandomPrime()
        {
            int[] prime = {2, 3, 5, 7, 11, 13, 17,  19,  23,  29,  31,  37,  41,  43,  47,  53,  59,  61,  67,  71,
            73,  79,  83,  89,  97,  101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173,
            179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281,
            283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409,
            419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541,
            547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659,
            661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809,
            811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941,
            947, 953, 967, 971, 977, 983, 991, 997 };

            var ind = DateTime.Now.Millisecond % prime.Length;
            return prime[ind];
        }

        // Генерация ключа генератора
        public static (int, int, int n, int x) GenerateKey()
        {
            int p = 839; // TrueRandomPrime();
            int q = 733; // TrueRandomPrime();

            int n = p * q;
            int x;
            int r = DateTime.Now.Millisecond % n;
            x = PrimeNumbers.MutuallyPrimeNumber(n);
            // Console.WriteLine(x.ToString());
            return (p, q, n, x);
        }

        // Возведение в степень
        public static int Power(int a, int b)
        {
            int result = 1;
            for(int i = 0; i < b; i++)
            {
                result *= a;
            }
            return result;
        }

        // Получение случайного числа сборкой из 8 бит
        public static int GetRandom()
        {
            int result = 0;

            for (int j = 0, t = 1; j < 8; j++, t*=2)
            {           
                result += t * GetBit(j);
            }

            return result;
        }

        // Получение i-го элемента последовательности
        public static int GetBit(int i)
        {
            (int p, int q, int n, int x) = GenerateKey();
             
            int x0 = (x * x) % n;

            int step = Power(2, i) % ((p - 1) * (q - 1));
            int xi = Power(x0, step);

            return System.Math.Abs(xi % 2);
        }
    }
}
