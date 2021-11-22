using Cryptography.Algorithms.RandomNumbers;
using System;

namespace Cryptography.Programm
{
    class Randomizer
    {
        public int GetRandom(string genname)
        {
            switch (genname)
            {
                case "BBS":
                    return BlumBlumShub.GetRandom();
                default:
                    throw new Exception("Нет такого генератора");
            }
        }
    }
}
