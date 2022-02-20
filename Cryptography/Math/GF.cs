using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptography.Math
{
    public static class GF
    {
        public static byte Mult(byte a, byte b)
        {
            byte p = 0;
            byte counter;
            byte h_bit;
            for (counter = 0; counter < 8; counter++)
            {
                if ((b & 1) == 1)
                {
                    p ^= a;
                }
                h_bit = (byte)(a & 0x80);
                a <<= 1;
                if (h_bit == 1)
                    a ^= 0x1b; /* x^8 + x^4 + x^3 + x + 1 */
                b >>= 1;
            }
            return p;
        }
    }
}
