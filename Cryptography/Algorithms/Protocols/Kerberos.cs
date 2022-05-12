using Cryptography.Algorithms.Crypt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptography.Algorithms.Protocols
{
    public static class Kerberos
    {
        public static byte[] Alice()
        {
            String text = "G+C7jRvHqRvHqRvavxvgtJAbzJkb2r8b4KaAG+CvoBvgu40b4KijG+C0kBvgtp0bxYYb4KOzG+C2nRvFhhvgr6Ab3LYbLBvcthvakxvavxvgr6Abx6k=|G8u7G8KpG8KpG+C7kRvElhvgsYMb4LuRG+C2uxvVjRvLuxvNqBvElhvcshvgq7wb3p8b3LIb4Ku8G9WNG9a6G+C6lRvWuhs8G+C7kRvLuxvCqQ==";
            var texts = text.Split("|");
            var basedText = Convert.FromBase64String((texts[0]));
            var utfText = Encoding.UTF8.GetString(basedText);
            return basedText;
        }

        public static void OutputBytes(byte[] bytes)
        {
            foreach(byte b in bytes)
            {
                System.Console.Write(b + " ");        
            }
            System.Console.WriteLine();
        }

        public static byte[] DecryptBytes(byte[] text)
        {
            (int x, int y, int g, int p) = Getkey();
            var decryptedText = ElGamal.Decrypt(text, x, p);
            return decryptedText;
        }

        public static byte[] Encrypt(byte[] text)
        {
            (int x, int y, int g, int p) = Getkey();
            var cryptedText = ElGamal.Encrypt(text, p, g, y);
            return cryptedText;
        }

        private static (int, int, int, int) Getkey()
        {
            return (5, 106, 3, 137);
        }
    }
}
