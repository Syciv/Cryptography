using System;
using System.Collections.Generic;
using System.Text;
using static Cryptography.Math.PrimeNumbers;

namespace Cryptography.Math
{
    // Эллиптическая кривая вида y^3 = x^3 ax + b (mod p)
    public class EllipticCurve
    {
        public int a, b, p, num;
        // 
        public ECPoint[] points;

        public EllipticCurve(int a, int b, int p)
        {
            this.a = a;
            this.b = b;
            this.p = p;

            if (a >= p || b >= p || Module(4 * a * a * a + 27 * b * b, p) == 0)
            {
                throw new Exception("Нельзя создать кривую с такими a и b");
            }
        }

        // Инициализация точек кривой
        public void Init()
        {
            points = new ECPoint[1000];

            num = 0;
            int i = 0;
            for (int x = 0; x <= p; x++)
            {
                int r = Module(x * x * x + a * x + b, p);
                for (int y = 0; y <= p; y++)
                {
                    int l = Module(y * y, p);
                    if (l == r)
                    {
                        points[i] = new ECPoint(x, y, a, b, p);
                        i++;
                        num++;
                    }
                }
               
            }
            points[num] = new ECPoint(0, 0, a, b, p);
            num++;
        }

        public bool Contains(int x, int y)
        {
            for(int i=0;i<num;i++)
            {
                if(points[i].x == x && points[i].y == y)
                {
                    return true;
                }
            }
            return false;
        }

        // Вывод точек
        public void ShowPoints()
        {
            Console.WriteLine("Точки кривой y^3 = x^3 + ax + b");
            Console.WriteLine("Точек: {0}", num + 1);
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine("({0}, {1})", points[i].x, points[i].y);
            }
            // Console.WriteLine("O (y - inf)");
        }

        public ECPoint GetRandomPoint()
        {
            var rand = new Random();
            int i = rand.Next() % num;

            return points[i];
        }

        public ECPoint GetPoint(int i)
        {
            return points[i];
        }

        public int GetNum(int x, int y)
        {
            for(int i=0; i<num; i++)
            {
                if(points[i].x == x && points[i].y == y)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
