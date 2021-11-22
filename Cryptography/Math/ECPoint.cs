using System;
using static Cryptography.Math.PrimeNumbers;

namespace Cryptography.Math
{
    public class ECPoint
    {
        public int x, y,a, b, p, ord;

        public ECPoint(int x, int y, int a, int b, int p) 
        {
            this.x = x;
            this.y = y;
            this.p = p;
            this.a = a;
            this.b = b;
        }

        // Сложение точек
        public static ECPoint operator +(ECPoint p1, ECPoint p2)
        {
            int l;
            int x1 = p1.x, x2 = p2.x, y1 = p1.y, y2 = p2.y;
            int a = p1.a, b = p1.b, p = p1.p;

            if(x1==0 && y1 == 0)
            {
                return p2;
            }
            if (x2 == 0 && y2 == 0)
            {
                return p1;
            }

            if (x1 == x2 && y1 == y2)
            {
                l = (3 * x1 * x1 + a) * Reverse(2 * y1, p);
            }
            else
            {
                if(x1 == x2)
                {
                    return new ECPoint(0, 0, a, b, p);
                }
                l = (Module(y2 - y1, p) * Reverse(Module(x2 - x1, p), p));
            }
            l = Module(l, p);

            int x3, y3;

            x3 = Module(Module(l * l - x1, p) - x2, p);
            y3 = Module(l * Module(x1 - x3, p) - y1, p);

            return new ECPoint(x3, y3, a, b, p);
        }

        // Умножение точки на число
        public static ECPoint operator *(ECPoint p1, int num)
        {
            ECPoint m = p1;
            ECPoint result = m;
            // int x1, y1, x2, y2;
            for(int i = 1; i < num; i++)
            {
                result = result + m;// result.Add(m);
            }
            return result;
        }

        // Вычитание точек
        public static ECPoint operator -(ECPoint p1, ECPoint p2)
        {
            ECPoint result = p1 + (new ECPoint(p2.x, Module(0 - p2.y, p1.p), p2.a, p2.b, p2.p));// this.Add(new ECPoint(p2.x, Module(0-p2.y,p), p2.a, p2.b, p2.p));
            return result;
        }
    }
}
