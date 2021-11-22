using Cryptography.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptography.Algorithms.Crypt
{
    public class EllipticCurveCrypt
    {
        public static (int, ECPoint, ECPoint, EllipticCurve) GenerateKey()
        {
            int a = 1;
            int b = 3;
            int p = 41;
            EllipticCurve ecurve = new EllipticCurve(a, b, p);
            ecurve.Init();
            ECPoint G = ecurve.GetPoint(1); // GetRandomPoint();
            int nb = 50;
            ECPoint Pb = G*nb;

            return (nb, G, Pb, ecurve);
        }

        
        public static byte[] Encrypt(byte[] text, ECPoint G, ECPoint Pb, EllipticCurve ecurve, Dictionary<byte, int> dict)
        {
            byte[] cryptedText = new byte[text.Length * 2];
            int k;
            ECPoint c1, c2;
            byte m;
            var rand = new Random();
            // ecurve.ShowPoints();
            for (int i = 0, j = 0; i < text.Length; i++, j += 2)
            {
                m = text[i];
                ECPoint Pm = ecurve.GetPoint(dict[m]);
                k = rand.Next() % 41;
                c1 = G * k;// Mult(k);
                c2 = Pm + Pb * k;//  Pm.Add(Pb.Mult(k));

                cryptedText[j] = (byte)ecurve.GetNum(c1.x, c1.y);
                cryptedText[j + 1] = (byte)ecurve.GetNum(c2.x, c2.y);

            }
            return cryptedText;

        }

        public static byte[] Decrypt(byte[] text, int nb, EllipticCurve ecurve, Dictionary<byte, int> dict)
        {
            byte[] cryptedText = new byte[text.Length / 2];
            int  m;
            ECPoint c1, c2;
            for (int i = 0, j = 0; j < cryptedText.Length; i += 2, j++)
            {
                c1 = ecurve.GetPoint(text[i]);
                c2 = ecurve.GetPoint(text[i + 1]); ;

                ECPoint Pm = c2 - (c1 * nb);
                m = ecurve.GetNum(Pm.x,Pm.y);
                foreach (var it in dict)
                {
                    if (it.Value == m)
                        cryptedText[j] = it.Key;
                }
            }
            return cryptedText;
        }
    }
}
