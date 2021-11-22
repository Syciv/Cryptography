using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptography.Math
{
    public static class PrimeNumbers
    {
        public static int RandomPrime(int p)
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

            int i = 0;
            while(prime[i] < p)
            {
                i++;
            }

            var rand = new Random();
            return prime[rand.Next(i, prime.Length)];
        }

        // Проверить первообразный корень
        public static bool CheckRoot(int a, int p)
        {
            int Eiler = 0;
            // Вычисление функции Эйлера
            for(int i = 1; i < p; i++)
            {
                if (NOD(i, p) == 1)
                {
                    Eiler++;
                }
            }

            return ModularPower(a, Eiler, p)==1 ? true : false;
        }

        // Найти первообразный корень
        public static int GetRoot(int p)
        {
            for(int i=2; i < p; i++)
            { 
                if (CheckRoot(i, p))
                {
                    return i;
                }
            }
            return 0;
        }

        // Поиск наибольшего общего делителя
        private static int NOD(int m, int n)
        {
            while (m != n)
            {
                if (m > n)
                {
                    m = m - n;
                }
                else
                {
                    n = n - m;
                }
            }
            return n;
        }

        // Поиск взаимно простого числа с p
        public static int MutuallyPrimeNumber(int p)
        {
            int i;
            var rand = new Random();
            i = rand.Next(2, p);

            while (NOD(i, p) != 1) 
            {
                i++;
            }
            return i;
        }

        // Возведение в большую степень по модулю
        public static int ModularPower(int a, int b, int mod)
        {
            int result = 1;
            int _a = 1, t;

            while (b >= 1)
            {
                a *= _a;
                a %= mod;
                _a = a;
                t = b % 2;
                b = b / 2;
                if (t == 1)
                {
                    result *= a;
                    result = Module(result, mod);
                }
            }
            return Module(result, mod);
        }

        // Получение числа, обратного a по модулю b
        public static int Reverse(int a, int b)
        {
            int mod = b;
            // Находим обратное по расширенному алгоритму Евклида
            int q, r, x, y;

            int x2 = 1, x1 = 0, y2 = 0, y1 = 1;
            while (b > 0)
            {
                q = a / b;
                r = a - q * b;
                x = x2 - q * x1;
                y = y2 - q * y1;
                a = b;
                b = r;
                x2 = x1;
                x1 = x;
                y2 = y1;
                y1 = y;
            }
            x = x2;
            return Module(x, mod);
        }

        // Вычисление числа по модулю mod
        public static int Module(int a, int mod)
        {
            a %= mod;
            a = (a < 0) ? a + mod : a;
            return a;
        }
    }
}
