using System;
using System.IO;
using System.Text;
using Cryptography.Algorithms.Crypt;
using Cryptography.Algorithms.Signature;
using Cryptography.Algorithms.RandomNumbers;
using Cryptography.Math;

namespace Cryptography.Programm
{
    class Program
    {
        static void Output16(byte[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(Convert.ToString(text[i], 16) + " ");
            }
            Console.WriteLine();
        }

        static void Output10(byte[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(Convert.ToString(text[i], 10) + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Выберите действие:\n 1. Шифрование;\n 2. Электронная подпись;\n 3. ГПСЧ;\n 4. Хэш; ");
            int act = Convert.ToInt32(Console.ReadLine());

            if (act == 1)
            {
                Console.WriteLine("Выберите алгоритм:\n 1. Фейстель; 2. Магма; 3. Эль-Гамаль; 4. На основе Эллиптических кривых; 5. AES; ");
                int alg = Convert.ToInt32(Console.ReadLine());
                string currentCipher;
                switch (alg)
                {
                    case 1:
                        currentCipher = "FEISTELMODIFIED";
                        break;
                    case 2:
                        currentCipher = "MAGMA";
                        break;
                    case 3:
                        currentCipher = "ELGAMAL";
                        break;
                    case 4:
                        currentCipher = "ELLIPTICCURVE";
                        break;
                    case 5:
                        currentCipher = "AES";
                        break;
                    default:
                        return;
                }

                Crypter crypter = new Crypter();

                byte[] Text = Encoding.Default.GetBytes("vsem priveet vsem privet vsem privet");
                // byte[] Text = Encoding.Default.GetBytes("Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет");
                // byte[] Text = { (byte)5 };

                //Console.WriteLine("Исходный текст:\n" + Encoding.Default.GetString(Text));
                Console.WriteLine("Исходный текст:\n" );
                //Output16(Text);
                Output10(Text);
                byte[] CryptedText = crypter.Encrypt(Text, currentCipher);

                // Console.WriteLine("\nЗашифрованный текст:\n" + Encoding.Default.GetString(CryptedText));
                Console.WriteLine("\nЗашифрованный текст:\n");
                //Output16(CryptedText);
                Output10(CryptedText);

                byte[] DecryptedText = crypter.Decrypt(CryptedText, currentCipher, Text);
                // Console.WriteLine("\nРасшифрованный текст:\n" + Encoding.Default.GetString(DecryptedText));
                Console.WriteLine("\nРасшифрованный текст:\n");
                //Output16(DecryptedText);
                Output10(DecryptedText);

            }
            if (act == 2)
            {
                Console.WriteLine("Выберите алгоритм:\n 1. Эль-Гамаль; ");
                int alg = Convert.ToInt32(Console.ReadLine());
                string currentSign;
                switch (alg)
                {
                    case 1:
                        currentSign = "ELGAMAL";
                        break;
                    //case 2:
                    //    currentSign = "ELLIPTICCURVE";
                    //    break;
                    default:
                        return;
                }


                int r, s;
                byte[] Message = Encoding.Default.GetBytes("Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет Всем привет ");

                Signer signer = new Signer();

                (r, s) = signer.CreateSignature(Message, "ELGAMAL");

                Console.WriteLine("\nИсходный текст:\n" + Encoding.Default.GetString(Message));
                Console.WriteLine("Подпись: {0}, {1}", r, s);

                Console.WriteLine();
                bool result = signer.CheckSignature(Message, r, s, "ELGAMAL");

                if (result)
                {
                    Console.WriteLine("Подпись подтверждена.");
                }
                else
                {
                    Console.WriteLine("Подпись не подтверждена, вас обманывают");
                }
            }
            if (act == 3)
            {
                Console.WriteLine("Генератор Блюм-Блюм-Шуба: ");

                Randomizer randomizer = new Randomizer();

                int n = 256;
                int n1 = 50000;
                int[] Nums = new int[n];

                for (int i = 0; i < n; i++)
                {
                    Nums[i] = 0;

                }

                for (int i = 0; i < n1; i++)
                {
                    int r = randomizer.GetRandom("BBS");
                    Nums[r]++;
                }

                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("{0}: {1}", i, Nums[i]);
                }
            }
            if (act == 4)
            {
                Console.WriteLine("Хэш Стрибог: ");
                Hasher hasher = new Hasher();

                // Тестовый пример из ГОСТ 34.11
                byte[] message = { 0xfb, 0xe2,0xe5,0xf0,0xee,0xe3,0xc8,0x20,0xfb,0xea,0xfa,0xeb,0xef,0x20,0xff,0xfb,0xf0,0xe1,0xe0,0xf0,0xf5,0x20,0xe0,0xed,0x20,
                0xe8,0xec,0xe0, 0xeb, 0xe5, 0xf0, 0xf2, 0xf1, 0x20,0xff,0xf0,0xee,0xec,0x20,0xf1,0x20,0xfa,0xf2,0xfe,0xe5,0xe2,0x20,0x2c, 0xe8, 0xf6, 0xf3,
                0xed, 0xe2, 0x20, 0xe8, 0xe6, 0xee, 0xe1, 0xe8, 0xf0,0xf2,0xd1,0x20,0x2c, 0xe8, 0xf0, 0xf2,  0xe5, 0xe2, 0x20, 0xe5, 0xd1};
                Console.WriteLine("Исходное сообщение: ");
                Output16(message);
                var result = hasher.GetHash(message, "STRIBOG");
                Console.WriteLine("Хэш сообщения: ");
                Output16(result);

            }
        }
    }
}
