using Cryptography.Algorithms.Hash;
using System;


namespace Cryptography.Programm
{
    class Hasher
    {
        public byte[] GetHash(byte[] message, string hashName)
        {
            switch (hashName)
            {
                case "STRIBOG":
                    return Stribog.GetHash(message);
                default:
                    throw new Exception("Нет такого алгоритма");
            }
        }
    }
}
