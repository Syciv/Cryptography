using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptography.Algorithms.Crypt
{
    public static class FeistelModified
    {
        static int masterKey;
        static byte[] Key;


        public static void GenerateKey()
        {
            Random rand = new Random();

            // Случайное формирование мастер-ключа
            // masterKey = (byte)(rand.Next());

            masterKey = 0b1000101101110000;

            Key = new byte[4];
            Key[0] = (byte)((masterKey & 0b1111111100000000) / 256);
            Key[1] = (byte)((masterKey & 0b0001111111100000) / 32);
            Key[2] = (byte)((masterKey & 0b0000001111111100) / 4);
            Key[3] = (byte)(masterKey & 0b0000000011111111);
        }

        public static void ShowKey()
        {
            Console.WriteLine("\nМастер-ключ");

            Console.WriteLine(Convert.ToString(masterKey, 2));
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Ключ {0}:", i);
                Console.WriteLine(Convert.ToString(Key[i], 2));
            }
        }

        public static byte[] Encrypt(byte[] text)
        {
            GenerateKey();
            int last = 2 - text.Length % 2;

            byte[] cryptedText = new byte[text.Length + last];
            byte leftBlock;
            byte rightBlock;

            int i = 0;

            while (i < text.Length)
            {
                // Формирование левого блока
                leftBlock = (i<text.Length)? text[i]: (byte)0x00;

                // Формирование правого блока
                rightBlock = (i + 1 < text.Length) ? text[i + 1] : (byte)0x00;

                for (int step = 0; step < 4; step++)
                {
                    // Изменение правого блока
                    rightBlock = (byte)((rightBlock & 0b00001111) * 0b10000 + (rightBlock & 0b11110000) / 0b10000);

                    // Обработка левого блока
                    leftBlock = (byte)(leftBlock ^ Key[step]);

                    // Перемещение блоков
                    byte blocktemp = rightBlock;
                    rightBlock = leftBlock;
                    leftBlock = blocktemp;
                }

                // Окончание обработки блока
                cryptedText[i] = leftBlock;

                cryptedText[i + 1] = rightBlock;
                i += 2;
            }
            return cryptedText;
        }

        public static byte[] Decrypt(byte[] text)
        {
            return Encrypt(text);
        }
    }
}
