using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptography.Math
{
    public static class GF
    {

        public static byte Offset(byte num)
        {
            return (byte)((num << 1) + (num & 0x80) / 0x80);
        }

        public static byte MultBy02(byte num)
        {
            byte result;
            if(num < 0x80)
            {
                result = (byte)((num << 1));
            }
            else
            {
                result = (byte)((num << 1));
                result ^= 0x1b;
            }

            return result;
        }

        public static byte MultBy03(byte num)
        {
            return (byte)(MultBy02(num) ^ num);
        }

        public static byte MultBy09(byte num)
        {
            return (byte)(MultBy02(MultBy02(MultBy02(num))) ^ num);
        }

        public static byte MultBy0b(byte num)
        {
            return (byte)(MultBy02(MultBy02(MultBy02(num))) ^ MultBy02(num) ^ num);
        }

        public static byte MultBy0d(byte num)
        {
            return (byte)((MultBy02(MultBy02(MultBy02(num)))^ (MultBy02(MultBy02(num)) ^ num)));
        }

        public static byte MultBy0e(byte num)
        {
            return (byte)(MultBy02(MultBy02(MultBy02(num))) ^ MultBy02(MultBy02(num)) ^ MultBy02(num));
        }
    }
}
