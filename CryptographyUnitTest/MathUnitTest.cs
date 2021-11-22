using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cryptography.Math;

namespace CryptographyUnitTest
{
    [TestClass]
    public class MathUnitTest
    {
        [TestMethod]
        public void ReverseMod()
        {
            int num = 124;
            int mod = 18;
            int expected = 17;
            int result = PrimeNumbers.Reverse(num, mod);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(expected, result, "Не равны");
        }

        [TestMethod]
        public void Power()
        {
            int num = 2;
            int num2 = 81;
            int mod = 100;
            int expected = 26;
            int result = PrimeNumbers.ModularPower(num, num2, mod);
            Console.WriteLine(result);
            Assert.AreEqual(expected, result, "Не равны");
        }

        [TestMethod]
        public void ECMult()
        {
            ECPoint expected = new ECPoint(23,37,1,3,41);
            var ecurve = new EllipticCurve(1,3,41);
            var p = new ECPoint(1, 28, 1, 3, 41);
            ECPoint result = p.Mult(20);
            Console.WriteLine("({0}, {1})",result.x,result.y);
            // Assert.AreEqual(expected.x, result.x, "Не равны");
        }

        [TestMethod]
        public void ECAdd()
        {
            // ECPoint expected = new ECPoint(23, 37, 1, 3, 41);
            var ecurve = new EllipticCurve(1, 3, 41);
            ecurve.Init();

            for (int i = 0; i < 50; i++)
            {
                var p1 = ecurve.GetRandomPoint();
                var p2 = ecurve.GetRandomPoint();


                var p3 = p1.Add(p2);
                if (!ecurve.Contains(p3.x, p3.y))
                {
                    //Console.WriteLine("({0}, {1})", p1.x, p1.y);
                    //Console.WriteLine("({0}, {1})", p2.x, p2.y);
                    //Console.WriteLine("({0}, {1})", p3.x, p3.y);
                }
                Console.WriteLine(ecurve.Contains(p3.x, p3.y));
            }
            //var p = new ECPoint(1, 13, 1, 3, 41);
            //ECPoint result = p.Add(new ECPoint(1,13,1,3,41));
            //Console.WriteLine("({0}, {1})", result.x, result.y);
            // Assert.AreEqual(expected.x, result.x, "Не равны");
        }

        [TestMethod]
        public void ECSub()
        {
            ECPoint expected = new ECPoint(23, 37, 1, 3, 41);
            var ecurve = new EllipticCurve(1, 3, 41);
            var p = new ECPoint(11, 19, 1, 3, 41);
            ECPoint result = p.Sub(new ECPoint(5, 25, 1, 3, 41));
            Console.WriteLine("({0}, {1})", result.x, result.y);
            // Assert.AreEqual(expected.x, result.x, "Не равны");
        }

    }
}