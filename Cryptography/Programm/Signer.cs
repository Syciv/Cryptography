using System;
using System.Collections.Generic;
using System.Text;
using Cryptography.Algorithms.Signature;

namespace Cryptography.Programm
{
    class Signer
    {
        // Ключ
        int p, g, y, x;
        public (int, int) CreateSignature(byte[] message, string signname)
        {
            switch (signname)
            {
                case "ELGAMAL":             
                    (p, g, y, x) = ElGamalSignature.GenerateKey();
                    Console.WriteLine("Ключ для подписи: ");
                    Console.WriteLine("Закрытый ключ: x = {0}", x);
                    Console.WriteLine("Открытый ключ: p = {0}, g = {1}, y = {2}", p, g, y);
                    (int r, int s) = ElGamalSignature.CreateSignature(message, p, g, y, x);
                    return (r, s);
                case "ELLIPTICCURVE":
                    
                default:
                    throw new Exception("Нет такого алгоритма");
            }
        }

        // Проверка подписи
        public bool CheckSignature(byte[] message, int r, int s, string signname)
        {
            switch (signname)
            {
                case "ELGAMAL":
                    (p, g, y, x) = ElGamalSignature.GenerateKey();
                    bool check = ElGamalSignature.CheckSignature(message, r, s, p, g, y);
                    return check;
                case "ELLIPTICCURVE":
                default:
                    throw new Exception("Нет такого алгоритма");
            }
        }
    }
}
